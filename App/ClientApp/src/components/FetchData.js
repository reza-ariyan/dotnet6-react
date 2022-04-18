import React, {Component} from 'react';
import Select from 'react-select'
import NumericInput from 'react-numeric-input';


export class FetchData extends Component {
    static displayName = FetchData.name;
    static item;

    constructor(props) {
        super(props);
        this.state = {
            candidates: [],
            loading: true,
            selected: [],
            technologies: [],
            filters: {yearsOfExperience: 0, technology: '00000000-0000-0000-0000-000000000000'}
        };
        FetchData.item = this;
    }

    async componentDidMount() {
        return this.populateData();
    }

    static renderCandidatesTable(candidate) {

        //Years of experience change event
        async function setYearsOfExperience(value) {
            if (!value) return;
            await FetchData.item.setState({
                filters: {
                    technology: FetchData.item.state.filters.technology,
                    yearsOfExperience: value
                }
            });
            await FetchData.item.refreshCandidates();
        }

        //Technology change event
        async function technology(value) {
            await FetchData.item.setState({
                filters: {
                    technology: value,
                    yearsOfExperience: FetchData.item.state.filters.yearsOfExperience
                }
            });
            await FetchData.item.refreshCandidates();
        }

        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th colSpan={2}>
                            <Select options={FetchData.item.state.technologies} onChange={e => technology(e.value)}/>
                        </th>
                        <th colSpan={2}>
                            Years of experience:
                            <NumericInput min={0} max={50} value={FetchData.item.state.filters.yearsOfExperience}
                                          onChange={e => setYearsOfExperience(e)}/>
                        </th>
                    </tr>
                    <tr>
                        <th>Profile Picture</th>
                        <th>Full Name</th>
                        <th>Email</th>
                        <th>Action</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        candidate.map(c =>
                            <tr key={c.candidateId}>
                                <td><img className={"profile-picture"} src={c.profilePicture} alt={c.fullName}/></td>
                                <td>{c.fullName}</td>
                                <td>{c.email}</td>
                                <td>
                                    <button onClick={() => this.acceptCandidates(c.candidateId)}
                                            className={'accept'}>Accept
                                    </button>
                                    <button onClick={() => this.rejectCandidates(c.candidateId)}
                                            className={'reject'}>Reject
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderCandidatesTable(this.state.candidates);
        return (
            <div>
                <h1 id="tabelLabel">All Candidates</h1>
                <p>Here you can filter and accept candidates to hire or reject them.</p>
                {contents}
            </div>
        );
    }

    //Initializes data from server at first load
    async populateData() {
        await this.refreshCandidates();
        const technologiesResponse = await fetch('technologies');
        const technologies = await technologiesResponse.json();
        this.setState({loading: false, technologies: technologies});
    }

    //Refreshed candidates grid
    async refreshCandidates() {
        let filters = FetchData.item.state.filters;
        let queryString = new URLSearchParams(filters).toString();
        const response = await fetch('/candidates/?' + queryString);
        const candidates = await response.json();
        this.setState({candidates: candidates, loading: false});
    }

    //Accepts a candidate by candidate Id
    static async acceptCandidates(candidateId) {
        await FetchData.candidateAction('accept', candidateId);
    }

    //Rejects a candidate by candidate Id
    static async rejectCandidates(candidateId) {
        await FetchData.candidateAction('reject', candidateId);

    }

    //Does an action on candidate
    static async candidateAction(action, id) {
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
        };
        return await fetch('/candidates/' + action + '/' + id, requestOptions)
            .then(async () => {
                await this.item.refreshCandidates();
            });
    }
}
