import React, {useEffect, useState} from 'react';
import {Route, Routes, useLocation, useNavigate} from "react-router-dom";
import Profile from "./pages/Profile";
import Lineups from "./pages/Lineups";
import Premium from "./pages/Premium";
import News from "./pages/News";
import Start from "./pages/Start";
import {Layout, Menu, Button, theme, MenuProps, Typography, Affix} from 'antd';
import {
    CrownOutlined, FileDoneOutlined,
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UserOutlined, VideoCameraOutlined,
} from "@ant-design/icons";
import {Config} from "./conf";
import {useNavigationContext} from "./context/NavigationContextProvider";
import NotFound from "./pages/NotFound";
type MenuItem = Required<MenuProps>['items'][number];
const {Text,Link} = Typography;
const { Header, Sider, Content, Footer } = Layout;


export function getItem(
    label: React.ReactNode,
    key: React.Key,
    icon?: React.ReactNode,
    children?: MenuItem[],
    type?: 'group',
): MenuItem {
    return {
        key,
        icon,
        children,
        label,
        type,
    } as MenuItem;
}

const AppRouter: React.FC = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const navigationContext = useNavigationContext();
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer },
    } = theme.useToken();
    const onClick: MenuProps['onClick'] = (e) => {
        if(e.key==='/profile'){
            navigate('/profile/lineups');
            navigationContext.setNavigation({currentPage: e.key});
            return
        }
        navigate(e.key);
        navigationContext.setNavigation({currentPage: e.key});
    };
    const items: MenuItem[] = [
        getItem('Профиль', '/profile', <UserOutlined />),
        getItem('Lineups', '/lineups', <VideoCameraOutlined />),
        getItem('Premium', '/premium', <CrownOutlined />),
        getItem('Новости', '/news', <FileDoneOutlined/>),
    ];
    useEffect(()=>{
        if(location.pathname.split('/')[1]==='profile'){
            navigationContext.setNavigation({currentPage: '/profile'});
            return
        }
        navigationContext.setNavigation({currentPage: location.pathname});
    },[])

    return (
        <Layout style={{minHeight: Config.screenResolution.height,margin: 0}}>
            <Affix>
                <Sider trigger={null} collapsible collapsed={collapsed} style={{minHeight: Config.screenResolution.height,margin: 0}}>
                    <div
                        className="demo-logo"
                        style={{maxWidth: 200, height: 64, padding: '6px 4px 6px 4px'}}
                        onClick={()=> {
                            navigate('/');
                            navigationContext.setNavigation({currentPage: '/'})
                        }}
                    >
                        <div style={{width: '100%', height: '100%', backgroundColor: 'rgba(255,255,255,.2)', borderRadius: '10px'}}></div>
                    </div>
                    <Menu
                        theme="dark"
                        mode="inline"
                        defaultSelectedKeys={['start']}
                        items={items}
                        onClick={onClick}
                        selectedKeys={[navigationContext.navigation.currentPage]}
                    />
                </Sider>
            </Affix>
            <Layout>
                <Affix>
                    <Header style={{ padding: 0, background: colorBgContainer }}>
                        <Button
                            type="text"
                            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                            onClick={() => setCollapsed(!collapsed)}
                            style={{
                                fontSize: '16px',
                                width: 64,
                                height: 64,
                            }}
                        />
                    </Header>
                </Affix>
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 300,
                        background: colorBgContainer,
                        display: 'flex'
                    }}
                >
                    <Routes>
                        <Route path={"/profile/*"} element={<Profile/>}/>
                        <Route path={"/lineups"} element={<Lineups/>}/>
                        <Route path={"/premium"} element={<Premium/>}/>
                        <Route path={"/news"} element={<News/>}/>
                        <Route path={"/"} element={<Start/>}/>
                        <Route path="*" element={<NotFound/>} />
                    </Routes>
                </Content>
                <Footer style={{ textAlign: 'center', display: 'flex', alignItems: 'center', justifyContent: 'center', paddingTop: 15}}>
                    <Text>Warface Lineups ©2023 Created by <Link href="https://t.me/soolikss" target="_blank">Sooliks</Link></Text>
                </Footer>
            </Layout>
        </Layout>
    );
};

export default AppRouter;