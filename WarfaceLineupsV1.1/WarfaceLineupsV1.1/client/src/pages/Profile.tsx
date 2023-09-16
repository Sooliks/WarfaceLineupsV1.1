import React from 'react';
import {useUserContext} from "../context/UserContextProvider";
import {Space} from "antd";
import Auth from "../components/ui/auth/Auth";

const Profile: React.FC = () => {
    const userContext = useUserContext();

    return (
        <div style={{width: '100%', height: '100%'}}>
            {!userContext.user.isAuth ?
                <Auth/>
                :
                <Space>
                </Space>
            }
        </div>
    );
};

export default Profile;