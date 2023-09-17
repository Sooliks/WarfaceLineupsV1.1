import React, {createContext, useContext, useState} from 'react';
import {AccountType} from "../types/account";

type UserType = {
    account: AccountType
    isAuth: boolean
}
type UserContextProviderProps = {
    children: React.ReactNode
}
type UserContextType = {
    user: UserType;
    setUser: React.Dispatch<React.SetStateAction<UserType>>
}
const UserContext = createContext({} as UserContextType);
export const useUserContext = () => useContext(UserContext);

export const defaultUser: UserType = {
    account: {
        login: null,
        jwtToken: null,
        accountId: null,
        role: "member",
        isVerifiedAccount: null,
        isPremiumAccount: null
    },
    isAuth: false
}

const UserContextProvider = ({children}: UserContextProviderProps) => {
    const [user,setUser] = useState<UserType>(defaultUser);
    return (
        <UserContext.Provider value={{user,setUser}}>{children}</UserContext.Provider>
    );
};

export default UserContextProvider;