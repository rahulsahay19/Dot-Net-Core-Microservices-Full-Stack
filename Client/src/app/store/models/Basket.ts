export interface BasketItem {
    productId: string;
    productName: string;
    price: number;
    quantity: number;
    imageFile: string;
}

export interface Basket {
    userName: string;
    items: BasketItem[];
    totalPrice: number;
}