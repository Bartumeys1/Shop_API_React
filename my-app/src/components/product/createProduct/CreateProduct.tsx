import { ErrorMessage, Field, Formik } from "formik";
import { useEffect, useState } from "react";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { ICreateProduct } from "./store/type";


const CreateProduct =() => {

    const {GetCategoryList} = useActions();
    const {list , message , isLoaded  } = useTypedSelector(store=>store.category);
    const [createProduct, setCreateProduct] = useState<ICreateProduct>({
        name: "",
        categoryId: "red",
        description: "",
        price: 0,
      });

    useEffect(()=>{
        GetCategoryList();
    },[]);


    const sendDataHandler = (product:ICreateProduct) => {
        console.log(product);
    }

    const errorMessage = (fieldType: string, color: string = "red") => {
        return (
          <ErrorMessage name={fieldType}>
            {(msg) => <div style={{ color: color }}>{msg}</div>}
          </ErrorMessage>
        );
      };


      const selectItems = list.map( (cat , index)=>(
        <option key={cat.id} value={cat.id}>{cat.name}</option>
      ));

    return (
      <>
        <div className="container py-5 h-100">
          <h1 className="text-center"> Реєстрація крок 1 </h1>
          <div className="row d-flex justify-content-center h-100">
            <div className="col-md-7 col-lg-5 col-xl-5">
              <Formik
                initialValues={createProduct}
                onSubmit={sendDataHandler}
              >
                {(formik) => (
                  <form onSubmit={formik.handleSubmit}>
                    {/* Email input  */}
                    <div className="form-floating mb-4">
                      <Field
                        name="name"
                        type="text"
                        className="form-control form-control-lg"
                        value={formik.values.name}
                        placeholder="Ім'я"
                      />
                      <label htmlFor="email" className="form-label">
                        Ім'я
                      </label>
                      {errorMessage("name")}
                    </div>

                    {/* First name input  */}
                    <div className="form-floating mb-4">
                      <Field
                        name="description"
                        type="text"
                        className="form-control form-control-lg"
                        value={formik.values.description}
                        placeholder="description"
                      />
                      <label htmlFor="firstName" className="form-label">
                        description
                      </label>
                      {errorMessage("firstName")}
                    </div>

                    {/* Second name input  */}
                    <div className="form-floating mb-4">
                      <Field
                        name="price"
                        type="text"
                        className="form-control form-control-lg"
                        value={formik.values.price}
                        placeholder="price"
                      />
                      <label htmlFor="secondName" className="form-label">
                        price
                      </label>
                      {errorMessage("secondName")}
                    </div>

                    {/* Category field input  */}
                    <Field
                      as="select"
                      name="categoryId"
                      className="form-control form-control-lg"
                      onChange={formik.handleChange}
                    >
                      <option defaultValue={formik.values.categoryId}>
                        Default Value
                      </option>
                     {selectItems}
                    </Field>

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
}

export default CreateProduct;


