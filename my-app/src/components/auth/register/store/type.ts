export enum StepEnum {
    STEP_ONE,
    STEP_TWO
}
export interface IStepOneResult {
    email: string,
    firstName:string,
    secondName:string,
    phone?:string|number|undefined
}

export interface IStepOneProps {
    onStepNext: (model: IStepOneResult) => void
}

export interface IStepTwoResult {
    password: string,
    confirmPassword:string
}

export interface IStepTwoProps {
    onStepNext: (model: IStepTwoResult) => void
}

export interface IRegisterModel {
    stepOn?: IStepOneResult,
    stepTwo?: IStepTwoResult
}

export interface IRegisterStepOneValues{
    email: string,
    firstName:string,
    secondName:string,
    phone?:string|number|undefined
}

export interface IRegisterStepTwoValues{
    password: string|undefined,
    confirmPassword:string|undefined
}

