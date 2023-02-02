export interface ICategoryItem {
  id: number|string
  imageUrl:string,
  name: string,
  }

export interface ICategoryResponse {
  payload: Array<ICategoryItem>
  message:string,
}

export interface ICategoryState {
    list: Array<ICategoryItem>
    message:string,
    isLoaded:boolean
  }

  export enum CategoryActionTypes {
    CATEGORY_LIST = "CATEGORY_LIST",
    CREATE_CATEGORY = "CREATE_CATEGORY",
    SELECT_CATEGORY = "SELECT_CATEGORY",

  }

  export interface GetCategoryAction {
    type: CategoryActionTypes.CATEGORY_LIST,
    payload: ICategoryState
}

export interface SelectCategoryAction {
  type: CategoryActionTypes.SELECT_CATEGORY,
  payload: ICategoryState
}
export type CategoryActions= | GetCategoryAction|SelectCategoryAction;