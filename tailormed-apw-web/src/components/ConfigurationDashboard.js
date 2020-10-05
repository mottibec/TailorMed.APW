import React, { useEffect, useState } from "react";
import { getFoundationConfigurations } from '../api/api';
import Configuration from './Configuration';
import { Link } from 'react-router-dom';
import { Button } from '@material-ui/core';

const ConfigurationDashboard = () => {
    const [configurations, setConfigurations] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        getFoundationConfigurations()
            .then(newConfigurations => setConfigurations(newConfigurations))
            .then(() => setIsLoading(false));
    }, []);

    if (isLoading) {
        return <p>Loading ...</p>
    }
    const config = configurations.map(config => <Configuration config={config} />);
    return (
        <div>
            <Link to="/">Home</Link>
            {config}
            <Button variant="contained">Add New Foundation</Button>
        </div>
    )
}
export default ConfigurationDashboard;