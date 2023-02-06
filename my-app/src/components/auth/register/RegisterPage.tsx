import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import StepOnePage from "./steps/stepOne";
import StepTwoPage from "./steps/stepTwo";
import { IRegisterModel, IStepOneResult, IStepTwoResult, StepEnum } from "./store/type";


export interface ISendRegisterData {
     firstName?:string,
     lastName? :string,
     phoneNumber?:string,
     email :string,
     password :string,
     confirmPassword :string
}

const RegisterPage : React.FC = () => {

    const [step, setStep] = useState<StepEnum>(StepEnum.STEP_ONE);
    const [model, setModel] = useState<IRegisterModel>();
    const [isComplete, setIsComplete] = useState(false);
    const{RegistrationUser} = useActions();
    const navigator = useNavigate();

    useEffect(()=>{
        
        if (isComplete)
        {
            console.log("Complete",model); // send in Action
            handelRegistration();
        }

    },[isComplete]);

    const handelRegistration =async()=>{


        var result = await RegistrationUser({
            firstName:model?.stepOn?.firstName,
            lastName:model?.stepOn?.secondName,
            email:model?.stepOn?.email,
            phoneNumber:model?.stepOn?.phone,
            password:model?.stepTwo?.password,
            confirmPassword:model?.stepTwo?.confirmPassword
        } as ISendRegisterData);
        console.log("Result promis",result);
        if(Boolean(result) === true)
           await navigator("/login");
    }
    const hadleStepOneResult = (result: IStepOneResult) => {
        console.log("-----Step one result-----", result);
        setStep(StepEnum.STEP_TWO);
        setModel({...model,stepOn:result});
    }

    const hadleStepTwoResult = (result: IStepTwoResult) => {
        console.log("-----Step two result-----", result);
        setModel({...model,stepTwo:result});
        setIsComplete(true);
    }

    const data = ( 
        <>
            {step===StepEnum.STEP_ONE && <StepOnePage onStepNext={hadleStepOneResult}/>}
            {step===StepEnum.STEP_TWO && <StepTwoPage onStepNext={hadleStepTwoResult}/>}
            
        </>
    );

    return (
        <>
            {data}
        </>
    );
}

export default RegisterPage;
