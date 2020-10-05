import React, { useEffect, useState } from "react";
import { getFoundationPrograms } from '../api/api';
import Foundation from "./Foundation";
import { Link } from 'react-router-dom';
import List from '@material-ui/core/List';

const FoundationDashboard = () => {
    const [foundations, setfoundations] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        getFoundationPrograms()
            .then(newFoundations => setfoundations(newFoundations))
            .then(() => setIsLoading(false));
    }, []);

    if (isLoading) {
        return (<p>Loading...</p>);
    }
    return (
        <div className='foundation-dashboard'>
            <Link to="/config">Config</Link>
            <List>
                {foundations.map(foundation => <Foundation foundation={foundation} />)}
            </List>
        </div>
    );
}

export default FoundationDashboard;