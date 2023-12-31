import {$client, $clientAuth} from "../index";
import {CommentType} from "../../types/comment";
import {AxiosResponse} from "axios/index";

export namespace CommentsAPI {
    export const getComments = async (idLineup: number, page: number):Promise<CommentType[]> => {
        const {data} = await $client.get<CommentType[]>(`/comments?lineupId=${idLineup}&page=${page}`);
        return data;
    }
    export const addComment = async (idLineup: number, text: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/addcomment', {idLineup, text});
    }
    export const updateComment = async (idComment: number, newText: string): Promise<AxiosResponse> => {
        return await $clientAuth.post('/updatecomment', {idComment, newText});
    }
    export const deleteCommentAsUser = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletecomment/user?id=${id}`);
    }
    export const deleteCommentAsAdmin = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletecomment/admin?id=${id}`);
    }
    export const deleteCommentAsOwnerLineup = async (id: number): Promise<AxiosResponse> => {
        return await $clientAuth.delete(`/deletecomment/ownerlineup?id=${id}`);
    }

}