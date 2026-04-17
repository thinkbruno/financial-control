import axios from 'axios';

import type { CreateAxiosDefaults } from 'axios';

const config: CreateAxiosDefaults = {
    baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5000/api',
};

export const api = axios.create(config);