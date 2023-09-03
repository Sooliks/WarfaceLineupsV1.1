import Cookies, {CookieSetOptions} from "universal-cookie";


const config: CookieSetOptions = {
    maxAge: 86400,
    path: '/'
}

export const cookies = new Cookies(null, config);

