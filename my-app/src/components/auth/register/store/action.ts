import http from "../../../../http_common"
import { ISendRegisterData } from "../RegisterPage";

interface RegistrationResponse{
    isSuccess: boolean,
    message?: string
}

export const RegistrationUser = (model:ISendRegisterData) => async()=>{
    try{
        var result =await  http.post<RegistrationResponse>("api/Account/registration",model);
        
        const {data} = result;
        if(data.isSuccess)
        return Promise.resolve(true);

        return Promise.reject(false);
    }
    catch{
        return Promise.reject(false);
    }
}