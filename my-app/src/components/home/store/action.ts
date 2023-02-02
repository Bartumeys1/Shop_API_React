
import { Dispatch } from "react";
import http from '../../../http_common';
import { CategoryActions, CategoryActionTypes, ICategoryResponse } from "./type";

export const GetCategoryList = () => async (dispatch: Dispatch<CategoryActions>) => {
    try {
        const resp = await http.get<ICategoryResponse>("/api/Categories/GetAllCategories");
        const {data} = resp;
        console.log("GetAllCategories: ",data);
        
        dispatch({
            type: CategoryActionTypes.CATEGORY_LIST, 
            payload:{
                message:data.message,
                list:data.payload,
                isLoaded:true
            }});
    }
    catch(err: any)
    {

    }
}

