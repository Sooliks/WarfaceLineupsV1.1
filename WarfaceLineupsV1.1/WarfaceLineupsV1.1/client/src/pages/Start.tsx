import React, {CSSProperties} from 'react';
import {Button, Carousel, Space, Typography} from "antd";
import {useNavigate} from "react-router-dom";
import {useNavigationContext} from "../context/NavigationContextProvider";

const { Text, Title } = Typography;
const Start: React.FC = () => {
    const navigate = useNavigate();
    const navigationContext = useNavigationContext();

    const insideContentStyle: CSSProperties = {
        height: 350,
        lineHeight: '350px',
        background: '#1e1d1d',
        textAlign: 'center',
    }
    return (
        <div style={{width: '100%', height: '100%'}}>
            <div style={{width: '100%'}}>
                <Carousel autoplay style={{width: '100%'}}>
                    <Space>
                        <div style={insideContentStyle}>
                            <Text style={{fontSize: '14pt'}}>Warface Lineups - это сайт где вы можете загрузить свои раскидки и в будущем сетапы, а также искать их</Text>
                        </div>
                    </Space>
                    <Space>
                        <div style={insideContentStyle}>
                            <Text style={{fontSize: '14pt'}}>Warface Tracker - это сервис для просмотра статистики игры</Text>
                        </div>
                    </Space>
                </Carousel>
            </div>
            <Space direction={"vertical"} style={{display:'flex', alignItems: 'center'}}>
                <Title>Warface Lineups</Title>
                <Space direction={"horizontal"}>
                    <Button
                        type={"primary"}
                        onClick={()=> {
                            navigationContext.setNavigation({currentPage: "/profile"});
                            navigate("/profile");
                        }}
                        size={"large"}
                    >
                        Начать
                    </Button>
                </Space>
            </Space>
        </div>
    );
};

export default Start;