import React, {createContext, useContext, useState} from 'react';


type NavigationType = {
    currentPage: string
}
type NavigationContextProviderProps = {
    children: React.ReactNode
}

type NavigationContextType = {
    navigation: NavigationType;
    setNavigation: React.Dispatch<React.SetStateAction<NavigationType>>
}
const NavigationContext = createContext({} as NavigationContextType)

export const useNavigationContext = () =>  useContext(NavigationContext);
const NavigationContextProvider = ({children}:NavigationContextProviderProps) => {
    const [navigation,setNavigation] = useState<NavigationType>({
        currentPage: '/start'
    });
    return (
        <NavigationContext.Provider value={{navigation,setNavigation}}>{children}</NavigationContext.Provider>
    );
};

export default NavigationContextProvider;