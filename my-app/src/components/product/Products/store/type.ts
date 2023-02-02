export interface IProductItem {
    id: number,
    name: string,
    description: string,
    price: number

    
  }

  export interface IProductsByCategory{
    CategoryId:number
  }

export interface IProductResponse {
    payload: Array<IProductItem>,
    current_page: number,
    total: number,
    last_page: number
}

export interface IProductState {
    list: Array<IProductItem>
    current_page: number,
    total: number,
    count_pages: number,
    isLoaded:boolean
}
export interface IProductSearch {
    name?: string,
    category?:string,
    page?: number|string|null
    count?:number|string|null
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