export interface IProductItem {
    id:string | number,
    name:string,
    price: number,
    images: Array<IImageItem>,
    description:string
  }
  interface IImageItem{
    imageUrl?:string,
    name?:string,
    priority: string| number
}


export interface IProductResponse {
    payload: Array<IProductItem>,
}

export interface IProductState {
    list: Array<IProductItem>
    isLoaded:boolean
}

  export enum ProductActionTypes {
    PRODUCT_LIST = "PRODUCT_LIST",
    SET_CURRENT_PRODUCT = "SET_CURRENT_PRODUCT",
    CREATE_PRODUCT = "CREATE_PRODUCT",
    PRODUCT_PAGE = "PRODUCT_PAGE",
  }

  export interface GetProductAction {
    type: ProductActionTypes.PRODUCT_LIST,
    payload: IProductState
}
export type ProductActions= | GetProductAction;