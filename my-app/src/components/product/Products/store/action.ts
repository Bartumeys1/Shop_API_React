import { ProductActions, IProductResponse, ProductActionTypes, IProductSearch, IProductsByCategory } from './type';
import { Dispatch } from "react";
import http from '../../../../http_common';

// export const GetProductList = (search: IProductSearch) => async (dispatch: Dispatch<ProductActions>) => {
//     try {
//         const resp = await http.get<IProductResponse>("/api/products",{params: search});
//         const {data} = resp;
//         dispatch({
//             type: ProductActionTypes.PRODUCT_LIST, 
//             payload: {
//                 list: data.data,
//                 count_pages: data.last_page,
//                 current_page: data.current_page,
//                 total: data.total,
//                 isLoaded:true
//             }});

//     }
//     catch(err: any)
//     {

//     }
// }



export const GetProductsByCategoryList = (category: IProductsByCategory) => async (dispatch: Dispatch<ProductActions>) => {
    try {
        const resp = await http.post<IProductResponse>("/api/Products/GetAllByCategory",category);
        const {data} = resp;
        console.log("GetAllCategories: ",data);
        
        dispatch({
            type: ProductActionTypes.PRODUCT_LIST, 
            payload: {
                list:data.payload,  
                count_pages: 1,
                current_page: 1,
                total: 1,
                isLoaded:true
            }});

            return Promise.resolve();
    }
    catch(err: any)
    {
        return Promise.reject();
    }
}