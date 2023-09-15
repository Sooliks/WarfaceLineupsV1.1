
import {$clientAuth} from "../index";
import {NotificationType} from "../../types/notification";
import {AxiosResponse} from "axios";

export namespace NotificationsAPI {
    export const getNotificationsAsOwner = async (): Promise<NotificationType[]> => {
        const {data} = await $clientAuth.get<NotificationType[]>('/notifications')
        return data;
    }
    export const deleteNotificationAsOwner = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletenotification?id=${id}`);
    }
    export const getCountNotificationAsOwner = async (): Promise<number> => {
        const {data} = await $clientAuth.get<number>('/countnotifications');
        return data;
    }
}