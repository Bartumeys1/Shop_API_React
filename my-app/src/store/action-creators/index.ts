import * as UserActionsCreators from "../../components/auth/login/store/action";
import * as CategoryActionCreators from "../../components/home/store/action";
import * as ProductActionCreators from "../../components/product/Products/store/action";

const actions = {
    ...ProductActionCreators,
    ...UserActionsCreators,
    ...CategoryActionCreators
}
export default actions;