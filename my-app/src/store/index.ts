import {applyMiddleware, combineReducers, createStore} from "redux";
import {composeWithDevTools} from "redux-devtools-extension";
import thunk from "redux-thunk";
import { userReducer } from "../components/auth/login/store/accountReducer";
import { productReducer } from "../components/home/store/productReducer";

export const rootReducer = combineReducers({
    product: productReducer,
    account: userReducer
});

export const store = createStore(rootReducer,composeWithDevTools(applyMiddleware(thunk)));