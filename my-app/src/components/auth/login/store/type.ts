export interface IUserLogin {
    email?: string,
    password?: string
}
export interface IGoogleExternalLogin {
    provider?: string,
    token: string
}

export interface IUserLoginResponse{
    token:string,
  }

export interface IUserState {
    name?:string,
    isAuth:boolean,
    roles:Array<string>
  }



  export enum AccountActionTypes {
    LOGIN_USER = "LOGIN_USER",
    LOGOUT_USER = "LOGOUT_USER",
  }

  export interface LoginUserAction {
    type: AccountActionTypes.LOGIN_USER,
    payload: IUserState
}

export interface LogoutUserAction {
    type: AccountActionTypes.LOGOUT_USER,
    payload: IUserState
}

export type AccountAuth= LoginUserAction | LogoutUserAction;