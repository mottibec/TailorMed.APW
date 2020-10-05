import React from 'react';
import './App.css';
import FoundationDashboard from "./components/FoundationDashboard";
import ConfigurationDashboard from "./components/ConfigurationDashboard";
import { BrowserRouter, Route, Switch } from 'react-router-dom';

const App = () => {
  return (
    <div className="App">
      <Switch>
        <Route path='/' exact component={FoundationDashboard} />
        <Route path="/config" component={ConfigurationDashboard} />
      </Switch>
    </div>
  );
}

export default App;