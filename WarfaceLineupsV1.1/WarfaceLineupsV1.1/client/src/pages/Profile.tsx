import React, {useEffect, useState} from 'react';
import {useUserContext} from "../context/UserContextProvider";
import {Menu, MenuProps, Space} from "antd";
import Auth from "../components/ui/auth/Auth";
import {HeartOutlined, MailOutlined, SettingOutlined, VideoCameraOutlined} from "@ant-design/icons";
import {getItem} from "../AppRouter";
import {Route, Routes, useLocation, useNavigate} from "react-router-dom";
import Lineups from "./pagesProfile/Lineups";
import Settings from "./pagesProfile/Settings";
import Favourites from "./pagesProfile/Favourites";
import Notifications from "./pagesProfile/Notifications";
import CreateLineup from "./pagesProfile/CreateLineup";


const Profile: React.FC = () => {
    const userContext = useUserContext();
    const [current, setCurrent] = useState('lineups');
    const location = useLocation();
    const navigate = useNavigate();

    const items: MenuProps['items'] = [
        getItem('Lineups', 'sub1', <VideoCameraOutlined />, [
            getItem('Ваши Lineups', 'lineups', null),
            getItem('Создать', 'addnewlineup', null),
        ]),
        {
            label: 'Настройки',
            key: 'settings',
            icon: <SettingOutlined/>
        },
        {
            label: 'Избранное',
            key: 'favorites',
            icon: <HeartOutlined/>,
        },
        {
            label: `Уведомления`,
            key: 'notifications',
            icon: <MailOutlined/>,
        },
    ]
    useEffect(()=>{
        setCurrent(location.pathname.split('/')[2])
    },[])
    const onClick: MenuProps['onClick'] = (e) => {
        navigate(e.key);
        setCurrent(e.key);
    };
    return (
        <div style={{width: '100%', height: '100%'}}>
            {!userContext.user.isAuth ?
                <Auth/>
                :
                <div>
                    <Menu
                        onClick={onClick}
                        selectedKeys={[current]}
                        mode="horizontal"
                        items={items}
                        defaultSelectedKeys={['lineups']}
                        defaultValue={'lineups'}
                    />
                    <Routes>
                        <Route path={"/lineups"} element={<Lineups/>}/>
                        <Route path={"/settings"} element={<Settings/>}/>
                        <Route path={"/favorites"} element={<Favourites/>}/>
                        <Route path={"/notifications"} element={<Notifications/>}/>
                        <Route path={"/addnewlineup"} element={<CreateLineup/>}/>
                    </Routes>
                </div>
            }
        </div>
    );
};

export default Profile;