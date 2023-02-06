import { ProductActions, IProductResponse, ProductActionTypes } from './type';
import { Dispatch } from "react";
import http from '../../../../http_common';

export const GetProductsByCategory = (categoryId:number) => async (dispatch: Dispatch<ProductActions>) => {
    try {
        const resp = await http.get<IProductResponse>("api/Products/GetAllByCategory?id="+categoryId);
        const {data} = resp;
        console.log("GetAllProducts: ",data);
        
        dispatch({
            type: ProductActionTypes.PRODUCT_LIST, 
            payload: {
                list:data.payload,  
                isLoaded:true
            }});

            return Promise.resolve(data);
    }
    catch(err: any)
    {
        return Promise.reject();
    }
}

interface IImageItem{
    name:string,
    priority: string|number,
    imageUrl:string
}
export interface ITestInterfaceImage{
    images:Array<IImageItem>,
    message:string
}

export const GetImages = (id:number)=>async () =>{

    try {
        const resp = await http.get<ITestInterfaceImage>("api/Products/GetAllImages?id="+id);
        const {data} = resp;
        console.log("images: ",data);
            return Promise.resolve<ITestInterfaceImage>(data);
    }
    catch(err: any)
    {
        return Promise.reject(err);
    }
}

