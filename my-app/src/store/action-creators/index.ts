import * as UserActionsCreators from "../../components/auth/login/store/action";
import * as ProductActionCreators from "../../components/home/store/action";

const actions = {
    ...ProductActionCreators,
    ...UserActionsCreators
}
export default actions;