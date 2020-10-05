import axios from 'axios';
import config from '../config/config';

export const getFoundationPrograms = async () => {
    try {
        const response = await axios.get(`${config.API_URL}/foundations`);
        return response.data;
    }
    catch (error) {
        console.error(error);
        throw error;
    }
}

export const getFoundationConfigurations = async () => {
    try {
        const response = await axios.get(`${config.API_URL}/configuration`);
        return response.data;
    }
    catch (error) {
        console.error(error);
        throw error;
    }
}

export const updateFoundationConfiguration = async (configuration) => {
    try {
        const response = await axios.get(`${config.API_URL}/foundations`);
        return response.data;
    }
    catch (error) {
        console.error(error);
        throw error;
    }
}

export const SetFoundationScraperRunStatus = async (faundationId, isEnabled) => {
    try {
        const response = await axios.post(`${config.API_URL}/configuration/${faundationId}`, isEnabled);
        return response.data;
    }
    catch (error) {
        console.error(error);
        throw error;
    }
}

export const DisableFoundationScraper = async (faundationId) => {
    return SetFoundationScraperRunStatus(faundationId, false)
}

export const EnableFoundationScraper = async (faundationId) => {
    return SetFoundationScraperRunStatus(faundationId, true)
}