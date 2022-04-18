import React, {Component} from 'react';
import {Route} from 'react-router-dom';
import {Layout} from './components/Layout';
import {FetchData} from './components/FetchData';
import {AcceptedCandidates} from "./components/AcceptedCandidates";

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={FetchData}/>
                <Route path='/accepted-candidates' component={AcceptedCandidates}/>
                <Route path='/fetch-data' component={FetchData}/>
            </Layout>
        );
    }
}
