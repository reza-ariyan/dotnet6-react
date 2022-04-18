import React, {Component} from 'react';

export class AcceptedCandidates extends Component {
    static displayName = AcceptedCandidates.name;

    constructor(props) {
        super(props);
        this.state = {
            candidates: [],
            loading: true,
        };
    }

    async componentDidMount() {
        return this.populateData();
    }

    static renderCandidatesTable(candidate) {

        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>Profile Picture</th>
                        <th>Full Name</th>
                        <th>Email</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        candidate.map(c =>
                            <tr key={c.candidateId}>
                                <td><img className={"profile-picture"} src={c.profilePicture} alt={c.fullName}/></td>
                                <td>{c.fullName}</td>
                                <td>{c.email}</td>
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
            : AcceptedCandidates.renderCandidatesTable(this.state.candidates);
        return (
            <div>
                <h1 id="tabelLabel">Accepted Candidates List</h1>
                <p>Here you can see all accepted candidates.</p>
                {contents}
            </div>
        );
    }

    //Get accepted candidates data from server
    async populateData() {
        const response = await fetch('candidates/accepted');
        const candidates = await response.json();
        this.setState({candidates: candidates, loading: false});
    }

}
