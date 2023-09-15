import {AxiosResponse} from "axios/index";
import {$client, $clientAuth} from "../index";

export namespace UserAPI {
    export const registration = async (login: string, email: string, password: string): Promise<AxiosResponse> => {
        return await $client.post('/registration', {login, email, password});
    }
    export const authorization = async (email: string, password: string): Promise<AxiosResponse> => {
        return await $client.post('/authorization', {email, password});
    }
}