import { useRef } from "react";
import { useDispatch } from "react-redux";
import { redirect, useNavigate } from "react-router-dom";
import http from "../../../http_common";


const CreateProduct =() => {

    const dispatch = useDispatch();
    const navigate = useNavigate();

    const inputRef1 = useRef<HTMLInputElement>(null);
    const inputRef2 = useRef<HTMLInputElement>(null);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const product={
            name: inputRef1.current?.value,
            detail: inputRef2.current?.value,
        }
        
        http.post("/api/products",product).then((res) => {
            dispatch({ type: "CREATE_PRODUCT", payload: res.data });

        navigate("/");
          });
        
    }

    return (
        <div>
        <h1 className="text-center">Створення продукту</h1>
    <form onSubmit={handleSubmit}  className="col-md-6 offset-md-3">
        <div className="mb-3">
            <label >Назва
            <input type="text"  className="form-control"  id="name" name="name" ref={inputRef1}/>
            </label>
        </div>
        <div className="mb-3">
            <label className="form-label">Опис
            <input type="text"  className="form-control"  id="description" name="description" ref={inputRef2}/>
            </label>
        </div>
        <button className=" btn btn-primary btn-lg" type="submit" >Додати товар</button>
    </form>
      </div>
    );
}

export default CreateProduct;


