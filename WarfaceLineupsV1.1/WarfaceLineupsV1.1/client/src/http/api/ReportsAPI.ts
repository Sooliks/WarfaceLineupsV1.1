import {AxiosResponse} from "axios";
import {$clientAuth} from "../index";
import {ReportType} from "../../types/report";

export namespace ReportsAPI {
    export const addReport = async (lineupId: number, typeReport: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/addreport', {lineupId, typeReport});
    }
    export const getReports = async (): Promise<ReportType[]> => {
        const {data} = await $clientAuth.get<ReportType[]>('/reports');
        return data;
    }
    export const setCompleteReport = async (reportId: number): Promise<AxiosResponse> => {
        return await $clientAuth.post('/setcompletereport', reportId);
    }
}