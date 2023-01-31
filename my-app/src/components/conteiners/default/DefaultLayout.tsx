import { Outlet } from "react-router-dom";
import Copyright from "../../copyright";
import DefaultHeader from "./DefaultHeader";


const DefaultLayout = () => {
  return(
<>
    <DefaultHeader />
    <div className="container">
      <Outlet />
    </div>
   
    <Copyright/>

  </>
  )
};

export default DefaultLayout;
