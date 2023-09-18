import React from 'react';
import {Button, Result} from "antd";
import {useNavigate} from "react-router-dom";

const NotFound: React.FC = () => {
    const navigate = useNavigate();
    return (
        <div style={{display: 'flex', alignItems: 'center', justifyContent: 'center', width: '100%', height: '100%'}}>
            <Result
                status="404"
                title="404"
                subTitle="Извините, страница, которую вы посетили, не существует."
                extra={<Button type="primary" onClick={()=> navigate('/')}>На главную</Button>}
            />
        </div>
    );
};

export default NotFound;