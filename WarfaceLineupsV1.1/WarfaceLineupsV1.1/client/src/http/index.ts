import axios from "axios";
import {Config} from "../conf";
import {cookies} from "../data/cookies";

const $client = axios.create({
    baseURL: Config.isDevelopment ? 'http://localhost:5258/api' : '/api'
})
const $clientAuth = axios.create({
    baseURL: Config.isDevelopment ? 'http://localhost:5258/api' : '/api'
})

const authInterceptor = (config: any) => {
    config.headers.authorization = `${cookies.get('jwt')}`;
    config.headers.login = `${cookies.get('login')}`
    return config;
}

$clientAuth.interceptors.request.use(authInterceptor)

export {
    $client,
    $clientAuth
}