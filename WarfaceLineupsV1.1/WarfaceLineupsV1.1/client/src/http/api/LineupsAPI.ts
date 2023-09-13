import {LineupsType} from "../../types/lineups";
import {$client} from "../index";

export namespace LineupsAPI {
    export const getLineups = async (): Promise<LineupsType[]> => {
        const {data} = await $client.get<LineupsType[]>(`/lineups`);
        return data;
    }
}