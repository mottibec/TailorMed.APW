const express = require('express');
const fs = require('fs');
const app = express();
const config = require('./config');
const cors = require('cors');

app.use(cors());

app.get('/foundations', (req, res) => {
    const foundations = fs.readFileSync(`${config.DATA_STORE_PATH}/foundations.json`, 'utf8');
    console.log('foundations', foundations);
    res.json(JSON.parse(foundations));
})

app.get('/configuration', (req, res) => {
    const configuration = fs.readFileSync(`${config.DATA_STORE_PATH}/configuration.json`, 'utf8');
    console.log('configuration', configuration);
    res.json(JSON.parse(configuration));
})

app.post('/configuration', (req, res) => {
    const configurationString = fs.readFileSync(`${config.DATA_STORE_PATH}/configuration.json`, 'utf8');
    const configuration = JSON.parse(configurationString);
    const newConfiguration = req.body;
    configuration.push(newConfiguration);
    fs.writeFile(`${config.DATA_STORE_PATH}/configuration.json`, JSON.stringify(configuration));
    res.status(200);
});

app.post('/configuration/:id', (req, res) => {
    const configurationString = fs.readFileSync(`${config.DATA_STORE_PATH}/configuration.json`, 'utf8');
    const configurations = JSON.parse(configurationString);
    const isEnabled = req.body;
    
    fs.writeFile(`${config.DATA_STORE_PATH}/configuration.json`, JSON.stringify(configuration));
    res.status(200);
})


app.listen(config.PORT, () => {
    console.log(`App listening at http://localhost:${config.PORT}`)
})