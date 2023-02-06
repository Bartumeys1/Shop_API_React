interface ICategoryProps {
  id: string | undefined;
  name: string;
  imageUrl: string;
  slug: string;
}

const CategoryCart: React.FC<ICategoryProps> = ({ id, name, imageUrl , slug }) => {
   const handelCilck = ()=>{
    console.log(id);

   }
   
  return (
    <>
      <div key={id} id={id} className="card h-100">
        <a href={`category/${slug}`}
          style={{ color: "black", textDecoration: "none" }}
          onClick={handelCilck}
        >
          <img src={imageUrl} className="card-img-top" alt={name} />
          <div className="card-body">
            <h5 className="card-title " style={{ textAlign: "center" }}>
              {name}
            </h5>
          </div>
        </a>
      </div>
    </>
  );
};

export default CategoryCart;
