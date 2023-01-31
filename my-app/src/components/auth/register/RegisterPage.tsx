import { useEffect, useState } from "react";
import StepOnePage from "./steps/stepOne";
import StepTwoPage from "./steps/stepTwo";
import { IRegisterModel, IStepOneResult, IStepTwoResult, StepEnum } from "./store/type";

const RegisterPage : React.FC = () => {

    const [step, setStep] = useState<StepEnum>(StepEnum.STEP_ONE);
    const [model, setModel] = useState<IRegisterModel>();
    const [isComplete, setIsComplete] = useState(false);

    useEffect(()=>{
        
        if (isComplete)
        console.log("Complete",model); // send in Action

    },[isComplete]);

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
