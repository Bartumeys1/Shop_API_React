import { useFormik } from "formik";

const SearchPanel:React.FC =({}) =>{

    // const formik = useFormik({
    //     initialValues: search,
    //     onSubmit: onSubmit,
    //   });
    
    //   const { handleSubmit, handleChange, values } = formik;
    //   function filterNonNull(obj: IProductSearch) {
    //     return Object.fromEntries(Object.entries(obj).filter(([k, v]) => !!v));
    //   }

    return(
        <>
        {/* <form onSubmit={handleSubmit}>
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
</form> */}
        </>
    );
}

export default SearchPanel;
