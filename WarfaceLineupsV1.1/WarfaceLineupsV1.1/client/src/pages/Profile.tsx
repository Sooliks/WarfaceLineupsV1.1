import React, {useState} from 'react';
import {useUserContext} from "../context/UserContextProvider";
import {Menu, MenuProps, Space} from "antd";
import Auth from "../components/ui/auth/Auth";
import {HeartOutlined, MailOutlined, SettingOutlined, VideoCameraOutlined} from "@ant-design/icons";
import {getItem} from "../AppRouter";

const Profile: React.FC = () => {
    const userContext = useUserContext();
    const [current, setCurrent] = useState('lineups');

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
    const onClick: MenuProps['onClick'] = (e) => {
        setCurrent(e.key);
    };
    return (
        <div style={{width: '100%', height: '100%'}}>
            {!userContext.user.isAuth ?
                <Auth/>
                :
                <Menu
                    onClick={onClick}
                    selectedKeys={[current]}
                    mode="horizontal"
                    items={items}
                    defaultSelectedKeys={['lineups']}
                />
            }
        </div>
    );
};

export default Profile;