import { Product } from "./Product";

export interface CatalogResponse {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: Product[];
}