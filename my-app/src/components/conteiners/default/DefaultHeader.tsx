import { Link } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

const DefaultHeader = () => {
  const { isAuth , name } = useTypedSelector((store) => store.account);
  const{Logout} = useActions();
  return (
    <header>
      <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <div className="container">
          <Link className="navbar-brand" to="/">
            Магазинчик
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarCollapse"
            aria-controls="navbarCollapse"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarCollapse">
            <ul className="navbar-nav me-auto mb-2 mb-md-0">
              <li className="nav-item">
                <Link className="nav-link active" aria-current="page" to="/">
                  Головна
                </Link>
              </li>
            </ul>
            <ul className="navbar-nav">
              {!isAuth ? (
                <>
                  <li className="nav-item">
                    <Link
                      className="nav-link"
                      aria-current="page"
                      to="/register"
                    >
                      Реєстрація
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" aria-current="page" to="/login">
                      Вхід
                    </Link>
                  </li>
                </>
              ) : (
                <>
                  <label className="nav-link">{name}</label>
                  <li className="nav-item">
                    <input type="button" className="nav-link" style={{border:0 , backgroundColor:"inherit"}} onClick={Logout} value="Вихід" />

                  </li>
                </>
              )}
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default DefaultHeader;
