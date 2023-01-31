import React from "react";
import { Link } from "react-router-dom";

const Copyright: React.FC<any> = (props) => {
    return(

        <div className="fixed-bottom"  style={{ display:"flex" , justifyContent:"center"}}>

            {'Copyright ©'}
            <Link color="inherit" to="/">
                Магазин головна сторінка 
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </div>

    )
}

export default Copyright;