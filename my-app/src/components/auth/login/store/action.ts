import jwtDecode from "jwt-decode";
import { Dispatch } from "react";
import http from '../../../../http_common';
import { AccountActionTypes, AccountAuth, IUserLogin, IUserLoginResponse, IUserState , IGoogleExternalLogin} from './type';

export  const Login = (user:IUserLogin) => async (dispatch: Dispatch<AccountAuth>) => {
    try {
        const resp = await http.post<IUserLoginResponse>("api/Account/login",user);
        const {data} = resp;
        setAuthUserByToken(data.token,dispatch);
    }
    catch(err: any)
    {
        console.log("Yap... Error",err);
    }
}
export  const GoogleExternalLogin = (googleData:IGoogleExternalLogin) => async (dispatch: Dispatch<AccountAuth>) => {
    console.log("data in google login ",googleData);
    try {
        
        const resp = await http.post<IUserLoginResponse>("api/Account/GoogleExternalLogin",googleData);
        const {data} = resp;
        setAuthUserByToken(data.token,dispatch);
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
              isAuth: false,
              roles:[]
            }});

            localStorage.removeItem("token");
    }
    catch(err: any)
    {
        console.log("Yap... Error",err);
    }
}

export const setAuthUserByToken = (token:string , dispatch:Dispatch<any>) => {

    setAuthToken(token);
    localStorage.setItem("token",token);

    const userInfo:IUserState = jwtDecode(token);

    dispatch({
        type: AccountActionTypes.LOGIN_USER, 
        payload: {
          name:userInfo.name,
          isAuth: true,
          roles:userInfo.roles
        }});
}

 const setAuthToken =(token:string)=>{

    if(token){
        http.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
    else
    {
        delete http.defaults.headers.common['Authorization'];
    }
 }