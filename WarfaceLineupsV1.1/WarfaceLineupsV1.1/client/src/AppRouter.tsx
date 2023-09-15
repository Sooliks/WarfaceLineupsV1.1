import React, {useState} from 'react';
import {Route, Routes} from "react-router-dom";
import Profile from "./pages/Profile";
import Lineups from "./pages/Lineups";
import Premium from "./pages/Premium";
import News from "./pages/News";
import Start from "./pages/Start";
import {Layout, Menu, Button, theme, MenuProps, Typography} from 'antd';
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UploadOutlined,
    UserOutlined,
    VideoCameraOutlined
} from "@ant-design/icons";
import {Config} from "./conf";
type MenuItem = Required<MenuProps>['items'][number];
const {Text} = Typography;
const { Header, Sider, Content, Footer } = Layout;

const items: MenuItem[] = [
    getItem('Профиль', '1', <UserOutlined />),
];
function getItem(
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

    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer },
    } = theme.useToken();

    return (
        <Layout style={{height: Config.screenResolution.height,margin: 0}}>
            <Sider trigger={null} collapsible collapsed={collapsed}>
                <div className="demo-logo" />
                <Menu
                    theme="dark"
                    mode="inline"
                    defaultSelectedKeys={['1']}
                    items={items}
                />
            </Sider>
            <Layout>
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
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 280,
                        background: colorBgContainer,
                    }}
                >
                    <Routes>
                        <Route path={"/profile"} element={<Profile/>}/>
                        <Route path={"/lineups"} element={<Lineups/>}></Route>
                        <Route path={"/premium"} element={<Premium/>}/>
                        <Route path={"/news"} element={<News/>}/>
                        <Route path={"/"} element={<Start/>}/>
                    </Routes>
                    fgfg
                </Content>
                <Footer style={{ textAlign: 'center', display: 'flex', alignItems: 'center', justifyContent: 'center', paddingTop: 15}}>
                    <Text>
                        Warface Lineups ©2023 Created by Sooliks
                    </Text>
                </Footer>
            </Layout>
        </Layout>
    );
};

export default AppRouter;