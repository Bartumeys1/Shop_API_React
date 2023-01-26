import { AccountActionTypes, AccountAuth, IUserState } from "./type";

const initialState : IUserState={
    name:"",
    roles:[]
};

export const userReducer = (state= initialState , action: AccountAuth ):IUserState =>{

    switch(action.type){
        case AccountActionTypes.LOGIN_USER:{
            return{
                ...state, 
                ...action.payload,
            }
        }
        case AccountActionTypes.LOGOUT_USER:{
            return{
                ...state, 
                ...action.payload,
            }
        }
        default:
            return state;
    }
}