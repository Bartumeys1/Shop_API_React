import { Link } from "react-router-dom";

const NoMatchPage =() => {
    return (
        <div>
        <h2>Тут нічого невидно!</h2>
        <p>
          <Link to="/">Вернутися на домашню сторінку</Link>
        </p>
      </div>
    );
}

export default NoMatchPage;