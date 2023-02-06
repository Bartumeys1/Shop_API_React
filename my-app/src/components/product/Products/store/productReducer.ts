import { IProductState, ProductActions, ProductActionTypes } from "./type";

const initialState : IProductState={
    list:[],
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