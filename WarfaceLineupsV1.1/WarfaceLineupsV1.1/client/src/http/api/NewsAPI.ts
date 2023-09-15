import {AxiosResponse} from "axios/index";
import {$client, $clientAuth} from "../index";
import {NewsType} from "../../types/news";

export namespace NewsAPI {
    export const addNews = async (title: string, text: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/publishnews', {title, text});
    }
    export const getNews = async (filter: number): Promise<NewsType[]> =>{
        const {data} = await $client.get<NewsType[]>('/news')
        return data;
    }
}