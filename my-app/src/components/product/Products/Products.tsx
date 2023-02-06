import { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { useActions } from "../../../hooks/useActions";
import ProductCart from "../../common/productCart/ProductCart";

const Products :React.FC= ({}) => {
  const { list} = useTypedSelector(
    (store) => store.product
  );
  const {}=useTypedSelector(store=>store.category);
  const { GetProductsByCategory , GetCategoryBySlug} = useActions();
  const params = useParams();

  useEffect(() => {
    hanfleLoadData();
  }, []);


  const hanfleLoadData = async () =>{
    var result = await GetCategoryBySlug(params.slug as string);
    await GetProductsByCategory(Number(result));
  }

  const listItems = list.map((item, index) => (
    <ProductCart
    key={index}
    id={item.id}
    name = {item.name}
    price ={item.price}
    description ={item.description}
    images = {item.images}/>
  ));


    return (
      <>
        <h1 className="text-center"> Каталог товарів</h1>

        <div className="row row-cols-1 row-cols-sm-2 row-cols-md-4" style={{margin:"5px 0px"}}>
           {listItems}
          </div>
      </>
    );
  // }
};

export default Products;
