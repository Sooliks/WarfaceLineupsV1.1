import React from 'react';
import AppRouter from "./AppRouter";
import {BrowserRouter} from "react-router-dom";
import classes from './Main.module.css'

import {ConfigProvider, theme} from "antd";
import NavigationContextProvider from "./context/NavigationContextProvider";

function App() {
  return (
      <ConfigProvider theme={{
          algorithm: theme.darkAlgorithm,
      }}>
          <NavigationContextProvider>
              <div className={classes.main}>
                <BrowserRouter>
                    <AppRouter/>
                </BrowserRouter>
              </div>
          </NavigationContextProvider>
      </ConfigProvider>
  );
}

export default App;
