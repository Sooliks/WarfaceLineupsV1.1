import {LineupType} from "../../types/lineup";
import {$client, $clientAuth} from "../index";
import {AxiosResponse} from "axios";

export namespace LineupsAPI {
    export const getLineups = async (page: number, typeSide: number, typeMap: number, typeFeature: number, typePlant: number, search?: string): Promise<LineupType[]> => {
        const {data} = search !== undefined ?
            await $client.get<LineupType[]>(`/lineups?page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}&Search=${search}`)
            :
            await $client.get<LineupType[]>(`/lineups?page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}`)
        return data;
    }
    export const getUnVerifiedLineups = async (page: number): Promise<LineupType[]> => {
        const {data} = await $clientAuth.get<LineupType[]>(`/unverifiedlineups?page=${page}`)
        return data;
    }
    export const getVerifiedLineupsByOwnerId = async (ownerId: number,page: number, typeSide: number, typeMap: number, typeFeature: number, typePlant: number, search?: string): Promise<LineupType[]> => {
        const {data} = search !== undefined ?
            await $client.get<LineupType[]>(`/verifiedlineupsbyownerid?ownerId=${ownerId}&page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}&Search=${search}`)
            :
            await $client.get<LineupType[]>(`/verifiedlineupsbyownerid?ownerId=${ownerId}&page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}`)
        return data;
    }
    export const getLineupsAsOwner = async (page: number, typeSide: number, typeMap: number, typeFeature: number, typePlant: number, search?: string): Promise<LineupType[]> => {
        const {data} = search !== undefined ?
            await $clientAuth.get<LineupType[]>(`/lineupsbyowner?page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}&Search=${search}`)
            :
            await $clientAuth.get<LineupType[]>(`/lineupsbyowner?page=${page}&TypeSide=${typeSide}&TypeGameMap=${typeMap}&TypeFeature=${typeFeature}&TypePlant=${typePlant}`)
        return data;
    }
    export const deleteLineupAsAdmin = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletelineup/admin?lineupId=${id}`);
    }
    export const deleteLineupAsUser = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletelineup/user?lineupId=${id}`);
    }
    export const publishLineup = async (lineupId: number): Promise<AxiosResponse> => {
        return await $clientAuth.post('/publishlineup',lineupId);
    }
    export const getVerifiedLineupById = async (id: number): Promise<LineupType> => {
        const {data} = await $client.get<LineupType>(`/lineup/${id}`);
        return data;
    }

}