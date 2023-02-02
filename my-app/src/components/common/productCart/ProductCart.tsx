
interface IProductProps{
    name:string,
    price: number,
    description:string
}


const ProductCart:React.FC<IProductProps> =({
    name,
    price,
    description,

}) =>{
console.log(name);

    return(
        <>
         <div className="card h-100">
      <img src="..." className="card-img-top" alt="name"/>
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