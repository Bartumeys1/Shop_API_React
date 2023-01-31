import { ErrorMessage, Field, Formik } from "formik";
import React, { useState } from "react";
import {
  IRegisterStepOneValues,
  IStepOneProps,
  IStepOneResult,
} from "../../store/type";
import { RegistrationStepOneSchema } from "../../store/validator/validation";


const StepOnePage: React.FC<IStepOneProps> = ({ onStepNext }) => {
  const [registerValue, setRegisterValue] = useState<IRegisterStepOneValues>({
    email: "",
    firstName: "",
    secondName: "",
    phone: "(000)-000-0000",
  });

  const onNext = (props: IStepOneResult) => {
    console.log("props: ", props);

    onStepNext({
      email: props.email,
      firstName: props.firstName,
      secondName: props.secondName,
      phone: props.phone,
    });
  };

  const errorMessage = (fieldType: string, color: string = "red") => {
    return (
      <ErrorMessage name={fieldType}>
        {(msg) => <div style={{ color: color }}>{msg}</div>}
      </ErrorMessage>
    );
  };

  return (
    <>
        <div className="container py-5 h-100">
          <h1 className="text-center"> Реєстрація крок 1 </h1>
          <div className="row d-flex justify-content-center h-100">
            <div className="col-md-7 col-lg-5 col-xl-5">
              <Formik
                initialValues={registerValue}
                validationSchema={RegistrationStepOneSchema}
                onSubmit={onNext}
              >
                {(formik) => (
                  <form onSubmit={formik.handleSubmit}>
                    {/* Email input  */}
                    <div className="form-outline mb-4">
                      <label htmlFor="email" className="form-label">
                        Електронна пошта
                      </label>
                      <Field
                        name="email"
                        type="text"
                        className="form-control form-control-lg"
                      />
                      {errorMessage("email")}
                    </div>

                    {/* First name input  */}
                    <div className="form-outline mb-4">
                      <label htmlFor="firstName" className="form-label">
                        Ім'я
                      </label>
                      <Field
                        name="firstName"
                        type="text"
                        className="form-control form-control-lg"
                      />
                      {errorMessage("firstName")}
                    </div>

                    {/* Second name input  */}
                    <div className="form-outline mb-4">
                      <label htmlFor="secondName" className="form-label">
                        Призвіще
                      </label>
                      <Field
                        name="secondName"
                        type="text"
                        className="form-control form-control-lg"
                      />
                      {errorMessage("secondName")}
                    </div>

                    {/* Phone field input  */}
                    <div className="form-outline mb-4">
                      <label htmlFor="phone" className="form-label">
                        Телефон
                      </label>
                      <Field
                        name="phone"
                        type="text"
                        className="form-control form-control-lg"
                        value={formik.values.phone}
                      />
                      {errorMessage("phone")}
                    </div>

                    {/* Submit button  */}
                    <button
                      type="submit"
                      className="btn btn-primary btn-lg btn-block"
                    >
                      Далі
                    </button>
                  </form>
                )}
              </Formik>
            </div>
          </div>
        </div>

    </>
  );
};

export default StepOnePage;
