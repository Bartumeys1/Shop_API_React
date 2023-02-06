import { useFormik } from "formik";
import {  useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { IGoogleExternalLogin, IUserLogin } from "./store/type";


const LoginPage =() => {
    
  const {Login , GoogleExternalLogin}=useActions();
  const {isAuth} = useTypedSelector(store=>store.account);
  const [loginData, setloginData] = useState<IUserLogin>({
    email:"",
    password:""
  });
const navigat = useNavigate();

    const handleLoginSuccess = (res: any) => {  
        const {credential} = res;
        
        const element:IGoogleExternalLogin ={
          provider : "Google",
          token: credential
        }

        GoogleExternalLogin(element);
      };
    
      useEffect(() => {
        const clientId =
          "193315193567-2q37oolq45ku9oqqmju6a2f8pbuuk2ih.apps.googleusercontent.com";
          window.google.accounts!.id.initialize({
          client_id: clientId,
          callback: handleLoginSuccess,
        });
    
        window.google.accounts!.id.renderButton(document.getElementById("loginGoogleBtn"),
        {theme: "outline", size: "Large"});
    
      }, []);

  
      const onSubmit = (values: IUserLogin) => {
        let user =values;
        Login(user); 
      };


      const formik = useFormik({
        initialValues: loginData,
        onSubmit: onSubmit,
      });

      const{handleSubmit , handleChange, values}=formik;

      if(isAuth)
      navigat("/");
      
    return (
        <>
  <div className="container py-5 h-100">
  <h1 className="text-center"> Вхід</h1>
    <div className="row d-flex align-items-center justify-content-center h-100">
      <div className="col-md-8 col-lg-7 col-xl-6">
        <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.svg" className="img-fluid" alt="Phone image"/>
      </div>
      <div className="col-md-7 col-lg-5 col-xl-5 offset-xl-1">
        <form onSubmit={handleSubmit}>
           {/* Email input  */}
          <div className="form-floating mb-4">
            <input type="email" id="email" name="email" className="form-control form-control-lg" placeholder="name@example.com"
            onChange={handleChange}
         value={values.email}/>
            <label htmlFor="email">Електрона пошта</label>
          </div>
      

           {/* Password input  */}
          <div className="form-floating mb-4">
            <input type="password" id="password" name="password" className="form-control form-control-lg" placeholder="Password"
             onChange={handleChange} 
             value={values.password} />
            <label className="form-label" htmlFor="password">Пароль</label>
          </div>

           {/* Submit button  */}
           <button type="submit" className="btn btn-primary btn-lg btn-block">Увійти</button>


          <div className="divider d-flex align-items-center my-4">
            <p className="text-center fw-bold mx-3 mb-0 text-muted">Або</p>
          </div>
            
            <div>
            <div id="loginGoogleBtn" ></div>

            </div>

        </form>
      </div>
    </div>
  </div>

        </>
    );
}

export default LoginPage;