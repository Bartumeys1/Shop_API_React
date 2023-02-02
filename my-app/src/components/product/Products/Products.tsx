import { useFormik } from "formik";
import qs from "qs";
import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import Pagination from "../../common/pagination/Pagination";
import http from "../../../http_common";
import Loader from "../../loader";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { useActions } from "../../../hooks/useActions";
import { IProductsByCategory, IProductSearch } from "./store/type";
import ProductCart from "../../common/productCart/ProductCart";


const Products :React.FC= ({}) => {
  const { list, isLoaded, count_pages, current_page, total } = useTypedSelector(
    (store) => store.product
  );
  const {  GetProductsByCategoryList} = useActions();
  const [searchParams, setSearchParams] = useSearchParams();
  const [search, setSearch] = useState<IProductSearch>({
    name: searchParams.get("name") || "",
    page: searchParams.get("page") || 1,
    count: searchParams.get("count") || 2,
  });


  useEffect(() => {
    const category:IProductsByCategory = {CategoryId:1}
    GetProductsByCategoryList(category);
    // GetProductList(search);
    // console.log(searchParams);
    
    console.log("list",isLoaded);
    
    
  }, []);

  const onSubmit = (values: IProductSearch) => {
    const filter = { ...values, page: 1 };
    setSearchParams(qs.stringify(filterNonNull(filter)));
    setSearch(filter);
  };

  const handleClickPaginate = (page: number) => {
    setSearch({ ...search, page: page });
    setSearchParams(qs.stringify(filterNonNull({ ...search, page: page })));
  }

  const formik = useFormik({
    initialValues: search,
    onSubmit: onSubmit,
  });

  const { handleSubmit, handleChange, values } = formik;
  function filterNonNull(obj: IProductSearch) {
    return Object.fromEntries(Object.entries(obj).filter(([k, v]) => !!v));
  }

  // const buttons = [];

  // for (let i = 1; i <= count_pages; i++) {
  //   buttons.push(i);
  // }

  // const navigateButtons = buttons.map((page) => (
  //   <li key={page} className="page-item">
  //     <Link
  //       className={classNames("page-link", { active: current_page === page })}
  //       onClick={() => {
  //         setSearch({ ...search, page });
  //       }}
  //       to={"?" + qs.stringify(filterNonNull({ ...search, page }))}
  //     >
  //       {page}
  //     </Link>
  //   </li>
  // ));

  const listItems = list.map((item, index) => (
    <ProductCart
    key={index}
    name = {item.name}
    price ={item.price}
    description ={item.description}
    />
    // <tr key={item.id}>
    //   <th>{index + 1}</th>
    //   <td> {item.name}</td>
    //   <td> {item.detail}</td>
    //   <td> {item.created_at}</td>
    //   <td>
    //     <button
    //       type="button"
    //       className="btn btn-danger"
    //       onClick={() => {
    //         http.delete(`/api/products/${item.id}`).then((res) => {
    //           if (res.statusText === "OK") GetProductList(search);
    //         });
    //       }}
    //     >
    //       Delete
    //     </button>
    //   </td>
    // </tr>
  ));

  // if (!isLoaded) {
  //   return <Loader />;
  // } else {
    return (
      <>
        <h1 className="text-center"> Головна сторінка</h1>
        <div className="text-center">
          <Link to={"add_product"} className={"btn btn-success"}>
            Створити новий продукт
          </Link>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="row">
            <div className="col">
              <div className="input-group rounded" style={{ width: `500px` }}>
                <input
                  id="name"
                  type="search"
                  name="name"
                  className="form-control mb-1 mt-1"
                  placeholder="Search name"
                  aria-label="Search"
                  aria-describedby="search-addon"
                  value={values.name}
                  onChange={handleChange}
                />

                <input
                  type={"submit"}
                  className="btn btn-primary"
                  value="Найти"
                  style={{ marginLeft: 10, marginRight: 10, borderRadius: 5 }}
                />
                <select
                  id="count"
                  name="count"
                  onChange={handleChange}
                  style={{
                    width: "50px",
                    position: "relative",
                    borderRadius: "5px",
                    border: "1px solid #ced4da",
                  }}
                >
                  <option defaultValue="2">2</option>
                  <option value="5">5</option>
                  <option value="10">10</option>
                  <option value="15">15</option>
                </select>
              </div>
            </div>
          </div>
        </form>


        <div className="row row-cols-1 row-cols-sm-2 row-cols-md-4" style={{margin:"5px 0px"}}>
           {listItems}
          </div>

        <div className="row-cols-1">
          <Pagination
          current_page={current_page}
          count_page={count_pages}
          onClick={handleClickPaginate}
        />
          </div>
      </>
    );
  // }
};

export default Products;
