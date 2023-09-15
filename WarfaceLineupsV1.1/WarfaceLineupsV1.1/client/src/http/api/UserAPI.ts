import {AxiosResponse} from "axios/index";
import {$client, $clientAuth} from "../index";

export namespace UserAPI {
    export const registration = async (login: string, email: string, password: string): Promise<AxiosResponse> => {
        return await $client.post('/registration', {login, email, password});
    }
    export const authorization = async (email: string, password: string): Promise<AxiosResponse> => {
        return await $client.post('/authorization', {email, password});
    }
    export const authorizationByJwt = async (login: string, jwtToken: string): Promise<AxiosResponse> => {
        return await $client.post('/authorizationbyjwt', {login, jwtToken});
    }
    export const getVerificationCode = async (): Promise<AxiosResponse> => {
        return await $clientAuth.get('/getverificationcode');
    }
    export const uploadVerificationCode = async (verificationCode: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/uploadverificationcode', verificationCode);
    }
}