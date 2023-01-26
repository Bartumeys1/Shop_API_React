import { IProductState, ProductActions, ProductActionTypes } from "./type";

const initialState : IProductState={
    list:[],
    current_page: 0,
    total:0,
    count_pages:0,
    isLoaded:false
};

export const productReducer = (state= initialState , action: ProductActions):IProductState =>{

    switch(action.type){
        case ProductActionTypes.PRODUCT_LIST:{
            return{
                ...state, 
                ...action.payload,
            }
        }
        default:
            return state;
    }
}