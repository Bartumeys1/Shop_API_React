import { useFormik } from "formik";
import { useState } from "react";
import { IRegisterStepTwoValues, IStepTwoProps, IStepTwoResult } from "../../store/type";
import { RegistrationStepTwoSchema } from "../../store/validator/validation";

const StepTwoPage: React.FC<IStepTwoProps> = ({ onStepNext }) => {
  const [registerValue, setRegisterValue] = useState<IRegisterStepTwoValues>({
    password: "",
    confirmPassword: "",
  });

  const onNext = (resp:IStepTwoResult) => {
    console.log("One step result");
    onStepNext({
      password: resp.password ,
      confirmPassword: resp.confirmPassword ,
    });
  };

  const formik = useFormik({
    initialValues: registerValue,
    validationSchema: RegistrationStepTwoSchema,
    onSubmit: onNext,
  });

  const { handleSubmit, handleChange, handleBlur, values } = formik;

  return (
    <>
   
        <div className="container py-5 h-100">
          <h1 className="text-center"> Реєстрація крок 2 </h1>
          <div className="row d-flex justify-content-center h-100">
            <div className="col-md-7 col-lg-5 col-xl-5">
              <form onSubmit={handleSubmit}>
                {/* Password input  */}
                <div className="form-floating mb-4">
                  <input
                    type="password"
                    id="password"
                    name="password"
                    className="form-control form-control-lg"
                    onChange={handleChange}
                    value={values.password}
                    onBlur={handleBlur}
                    placeholder="Password"
                  />
                                    <label className="form-label" htmlFor="password">
                    Пароль
                  </label>
                  {formik.touched.password && formik.errors.password ? (
                    <div style={{color:"red"}}>{formik.errors.password}</div>
                  ) : null}
                </div>

                {/* Password confirm input  */}
                <div className="form-floating mb-4">

                  <input
                    type="password"
                    id="confirmPassword"
                    name="confirmPassword"
                    className="form-control form-control-lg"
                    onChange={handleChange}
                    value={values.confirmPassword}
                    onBlur={handleBlur}
                    placeholder="confirmPassword"
                  />
                                    <label className="form-label" htmlFor="confirmPassword">
                    Повторити пароль
                  </label>
                  {formik.touched.confirmPassword &&
                  formik.errors.confirmPassword ? (
                    <div style={{color:"red"}}>{formik.errors.password}</div>

                  ) : null}
                </div>

                {/* Submit button  */}
                <button
                  type="submit"
                  className="btn btn-primary btn-lg btn-block"
                >
                  Далі
                </button>
              </form>
            </div>
          </div>
        </div>
    </>
  );
};
export default StepTwoPage;
