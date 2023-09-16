import React, {useState} from 'react';
import {Button, Checkbox, Form, Input, notification, Typography} from "antd";
import {LockOutlined, UserOutlined} from "@ant-design/icons";
import {UserAPI} from "../../../http/api/UserAPI";



const { Link } = Typography;


const Login : React.FC = () => {
    const [loading,setLoading] = useState<boolean>(false)
    const onFinish = (values: any) => {
        setLoading(true);
        UserAPI.authorization(values.email, values.password).then(res=>{
            setLoading(false);
        }).catch(()=>{

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
                name="login"
                rules={[{ required: true, message: 'Пожалуйста введите логин!' }]}
            >
                <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="Логин" />
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

