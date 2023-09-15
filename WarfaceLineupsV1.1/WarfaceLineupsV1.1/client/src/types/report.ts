import {LineupType} from "./lineup";

export type ReportType = {
    id: number
    status: string
    senderLogin: string
    lineup: LineupType
    typeReport: string
}