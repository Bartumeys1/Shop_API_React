import { useEffect, useState } from "react";
import { useActions } from "../../../hooks/useActions";
import { ITestInterfaceImage } from "../../product/Products/store/action";
import { IProductItem } from "../../product/Products/store/type";




const ProductCart:React.FC<IProductItem> =({
    id,
    name,
    price,
    description,
    images

}) =>{

    const [image,setImage] = useState<string>();

    const {GetImages} =useActions();
      useEffect(()=>{
        //load images
        handlerImages();
    },[]);

    const handlerImages = async()=>{

        setImage(images[0]?.imageUrl)
    }

    return(
        <>
         <div className="card h-100">
      <img src={image} className="card-img-top" alt={name}/>
      <div className="card-body">
        <h5 className="card-title">{name}</h5>
        <p className="card-text">{description}</p>
        <p>Ціна: {price} грн.</p>
      </div>
      </div>
        </>
    );
}

export default ProductCart;