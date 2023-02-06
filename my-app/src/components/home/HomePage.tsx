
import { Link } from "react-router-dom";
import ViewCategories from "../category";

const HomePage = () => {


    return (
     <>
     <h1 className="text-center"> Головна сторінка (Каталоги)</h1>
     <div className="text-center">
          <Link to={"add_product"} className={"btn btn-success"}>
            Створити новий продукт
          </Link>
        </div>
        <ViewCategories/>
     </>
    );

};

export default HomePage;
