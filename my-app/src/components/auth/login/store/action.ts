import jwtDecode from "jwt-decode";
import { Dispatch } from "react";
import http from '../../../../http_common';
import { AccountActionTypes, AccountAuth, IUserLogin, IUserLoginResponse, IUserState , IGoogleExternalLogin} from './type';

export  const Login = (user:IUserLogin) => async (dispatch: Dispatch<AccountAuth>) => {
    try {
        const resp = await http.post<IUserLoginResponse>("api/Account/login",user);
        const {data} = resp;
        const userInfo:IUserState = jwtDecode(data.token);

        dispatch({
            type: AccountActionTypes.LOGIN_USER, 
            payload: {
              name:userInfo.name,
              roles:userInfo.roles
            }});

    }
    catch(err: any)
    {
        console.log("Yap... Error",err);
    }
}
export  const GoogleExternalLogin = (googleData:IGoogleExternalLogin) => async (dispatch: Dispatch<AccountAuth>) => {
    console.log("data in google login ",googleData);
    try {
        googleData.provider="Google";
        const resp = await http.post<IUserLoginResponse>("api/Account/GoogleExternalLogin",googleData);
        const {data} = resp;
        const userInfo:IUserState = jwtDecode(data.token);

        dispatch({
            type: AccountActionTypes.LOGIN_USER, 
            payload: {
              name:userInfo.name,
              roles:userInfo.roles
            }});

    }
    catch(err: any)
    {
        console.log("Yap... Error",err);
    }
}

export  const Logout = () => async (dispatch: Dispatch<AccountAuth>) => {
    try {
          dispatch({
            type: AccountActionTypes.LOGOUT_USER, 
            payload: {
              name:"",
              roles:[]
            }});

    }
    catch(err: any)
    {
        console.log("Yap... Error",err);
    }
}