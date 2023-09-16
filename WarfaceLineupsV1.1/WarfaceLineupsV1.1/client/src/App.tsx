import React, {useEffect, useState} from 'react';
import AppRouter from "./AppRouter";
import {BrowserRouter} from "react-router-dom";
import classes from './Main.module.css'

import {ConfigProvider, Spin, theme} from "antd";
import NavigationContextProvider from "./context/NavigationContextProvider";
import {UserAPI} from "./http/api/UserAPI";
import {cookies} from "./data/cookies";
import {defaultUser, useUserContext} from "./context/UserContextProvider";


function App() {
    const[loading, setLoading] = useState<boolean>(true);
    const userContext = useUserContext();
    useEffect(()=>{
        UserAPI.authorizationByJwt(cookies.get('login'),cookies.get('jwt')).then(res=> {
            if(res.status === 200){
                userContext.setUser({...userContext.user, account: res.data})
                userContext.setUser({...userContext.user, isAuth: true})
                setLoading(false);
            }
            else {
                setLoading(false);
                userContext.setUser(defaultUser)
            }
        }).catch((e)=>{
            setLoading(false);
            userContext.setUser(defaultUser)
        })
    },[])
    if(loading){
        return <Spin size="large" className={classes.spinnerLoading} />
    }

    return (
      <ConfigProvider theme={{
          algorithm: theme.darkAlgorithm,
      }}>
          <NavigationContextProvider>
            <BrowserRouter>
                <AppRouter/>
            </BrowserRouter>
          </NavigationContextProvider>
      </ConfigProvider>
    );
}

export default App;
