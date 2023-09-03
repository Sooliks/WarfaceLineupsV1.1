import React from 'react';
import {Route, Routes} from "react-router-dom";
import Profile from "./pages/Profile";
import Lineups from "./pages/Lineups";
import Premium from "./pages/Premium";
import News from "./pages/News";
import Start from "./pages/Start";

const AppRouter: React.FC = () => {
    return (
        <Routes>
            <Route path={"/profile"} element={<Profile/>}/>
            <Route path={"/lineups"} element={<Lineups/>}></Route>
            <Route path={"/premium"} element={<Premium/>}/>
            <Route path={"/news"} element={<News/>}/>
            <Route path={"/"} element={<Start/>}/>
        </Routes>
    );
};

export default AppRouter;