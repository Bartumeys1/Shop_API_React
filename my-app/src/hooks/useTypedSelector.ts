import { useSelector } from "react-redux";
import { TypedUseSelectorHook } from "react-redux";
import { rootReducer } from "../store";


type rootState = ReturnType<typeof rootReducer>;
export const useTypedSelector : TypedUseSelectorHook<rootState> = useSelector;