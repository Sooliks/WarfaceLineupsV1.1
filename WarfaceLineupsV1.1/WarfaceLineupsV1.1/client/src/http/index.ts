import axios, {AxiosResponse, InternalAxiosRequestConfig} from "axios";
import {Config} from "../conf";
import {cookies} from "../data/cookies";

const $client = axios.create({
    baseURL: Config.isDevelopment ? 'http://localhost:5258/api' : '/api'
})
const $clientAuth = axios.create({
    baseURL: Config.isDevelopment ? 'http://localhost:5258/api' : '/api'
})

const authInterceptor = (config: InternalAxiosRequestConfig) => {
    config.headers.authorization = `${cookies.get('jwt')}`;
    config.headers.login = `${cookies.get('login')}`
    return config;
}


$clientAuth.interceptors.request.use(authInterceptor)
$clientAuth.interceptors.response.use((res: AxiosResponse)=>{

    return res;
}, (error) => {
    if (error.response.status === 401) {
        //window.location.reload();
        window.location.replace('/profile');
    }
    return Promise.reject(error);
})
$client.interceptors.response.use((res: AxiosResponse)=>{

    return res;
}, (error) => {
    return Promise.reject(error);
})


export {
    $client,
    $clientAuth
}