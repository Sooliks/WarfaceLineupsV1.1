import React, {useState} from 'react';
import {Button, Checkbox, Form, Input, notification, Typography} from "antd";
import {LockOutlined, UserOutlined} from "@ant-design/icons";
import {UserAPI} from "../../../http/api/UserAPI";
import {AxiosError} from "axios";
import {AccountType} from "../../../types/account";
import {useUserContext} from "../../../context/UserContextProvider";
import {cookies} from "../../../data/cookies";



const { Link } = Typography;


const Login : React.FC = () => {
    const [loading,setLoading] = useState<boolean>(false)
    const userContext = useUserContext();
    const onFinish = (values: any) => {
        setLoading(true);
        UserAPI.authorization(values.email, values.password).then(res=>{
            setLoading(false);
            const data: AccountType = res.data;
            cookies.set('jwt', data.jwtToken);
            cookies.set('login', data.login);
            userContext.setUser({account: data, isAuth: true})
        }).catch((error: AxiosError) =>{
            notification.error({
                message: "Уведомление",
                description: error.response!.data!.toString(),
                placement: "top"
            })
        })
    };



    return (
        <Form
            name="normal_login"
            initialValues={{ remember: true }}
            onFinish={onFinish}
            style={{width: '300px'}}
        >
            <Form.Item
                name="email"
                rules={[{ required: true, message: 'Пожалуйста введите email!' }]}
            >
                <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="Email" />
            </Form.Item>
            <Form.Item
                name="password"
                rules={[{ required: true, message: 'Пожалуйста введите пароль!' }]}
            >
                <Input
                    prefix={<LockOutlined className="site-form-item-icon" />}
                    type="password"
                    placeholder="Пароль"
                />
            </Form.Item>
            <Form.Item>
                <Form.Item name="remember" valuePropName="checked" noStyle>
                    <Checkbox>Запомнить меня</Checkbox>
                </Form.Item>
                <Link href="" style={{float: "right"}}>
                    Забыли пароль
                </Link>
            </Form.Item>

            <Form.Item>
                <Button type="primary" htmlType="submit" style={{width: '100%'}}>
                    Войти
                </Button>
            </Form.Item>
        </Form>
    );
};

export default Login;

