import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { Basket } from "../models/Basket";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class BasketService {
    private http = inject(HttpClient);
    baseUrl = 'http://localhost:8010/Basket';

    //initialize from local storage

    private basketSignal = signal<Basket | null>(this.loadBasket());
    basketCount = signal<number>(this.calcTotalItems(this.basketSignal()));

    private loadBasket(): Basket | null {
        const stored = localStorage.getItem('basket');
        return stored ? JSON.parse(stored) : null;
    }
    private saveBasket(basket: Basket){
        localStorage.setItem('basket', JSON.stringify(basket));
    }

    private calcTotalItems(basket: Basket | null): number {
        return basket ? basket.items.reduce((sum, i) => sum + i.quantity, 0): 0;
    }

    getBasket(username: string): Observable<Basket> {
        return this.http.get<Basket>(`${this.baseUrl}/${username}`);
    }

    updateBasket(basket: Basket): Observable<Basket> {
        return this.http.post<Basket>(this.baseUrl, basket);
    }

    deleteBasket(userName: string): Observable<any> {
        return this.http.delete(`${this.baseUrl}/${userName}`);
    }

    // call this after API returns updated basket
    setBasket(basket: Basket){
        this.basketSignal.set(basket);
        this.basketCount.set(this.calcTotalItems(basket));
        this.saveBasket(basket);
    }

    // expose current basket as signal getter
    get basket() {
        return this.basketSignal();
    }
}