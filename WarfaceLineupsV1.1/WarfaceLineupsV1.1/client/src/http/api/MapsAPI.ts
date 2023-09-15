import {AxiosResponse} from "axios";
import {$client, $clientAuth} from "../index";
import {MapType} from "../../types/map";

export namespace MapsAPI {
    export const addMap = async (name: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/addmap', name);
    }
    export const updateMap = async (id: number,name: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/updatemap', {id, name});
    }
    export const getMaps = async (): Promise<MapType[]> =>{
        const {data} = await $client.get<MapType[]>('/maps')
        return data;
    }
}