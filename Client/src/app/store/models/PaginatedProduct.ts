import { Product } from "./Product";

export interface PaginatedProducts {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: Product[];
}