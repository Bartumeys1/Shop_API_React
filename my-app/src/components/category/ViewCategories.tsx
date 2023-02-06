import { useEffect } from "react";
import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";
import CategoryCart from "../common/categoryCart/CategoryCart";

const ViewCategories: React.FC = ({}) => {
    
  const{GetCategoryList} = useActions();
  const { list} = useTypedSelector(store=>store.category);

  useEffect(()=>{
    GetCategoryList();
      
  },[]);

 const testCart = list.map((cat) => (
    <CategoryCart
    key={cat.id}
    id={cat.id.toString()}
    name={cat.name}
    imageUrl ={cat.imageUrl}
    slug = {cat.slug}
    />
  ));
   return (
    <>
        <div className="container">
  <div className="row row-cols-1 row-cols-sm-2 row-cols-md-4">
  {testCart}
  </div>
</div>
      </>
   );
}

export default ViewCategories;