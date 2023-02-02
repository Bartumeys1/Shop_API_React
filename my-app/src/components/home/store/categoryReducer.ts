import { CategoryActions, CategoryActionTypes, ICategoryState } from "./type";


const initialState : ICategoryState={
    list: [],
    message:"",
    isLoaded:false
};

export const categoryReducer = (state= initialState , action: CategoryActions):ICategoryState =>{

    switch(action.type){
        case CategoryActionTypes.CATEGORY_LIST:{
            return{
                ...state, 
                message: action.payload.message,
                list: action.payload.list,
            }
        }
        default:
            return state;
    }
}