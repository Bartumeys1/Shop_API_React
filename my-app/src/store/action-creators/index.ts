import * as UserLoginActionsCreators from "../../components/auth/login/store/action";
import * as UserRegistrationActionsCreators from "../../components/auth/register/store/action";
import * as CategoryActionCreators from "../../components/home/store/action";
import * as ProductActionCreators from "../../components/product/Products/store/action";

const actions = {
    ...ProductActionCreators,
    ...UserLoginActionsCreators,
    ...UserRegistrationActionsCreators,
    ...CategoryActionCreators
}
export default actions;