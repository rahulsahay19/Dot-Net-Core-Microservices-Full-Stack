import { Brand } from "./Brand";
import { Type } from "./Type";

export interface Product {
    id: string;
    name: string;
    summary: string | null;
    description: string | null;
    imageFile: string;
    brand: Brand;
    type: Type;
    price: number;
    createdDate: string;
}