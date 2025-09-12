import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CheckoutPayload } from "../models/CheckoutPayload";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class CheckoutService {
    private http = inject(HttpClient);
    private baseUrl = 'http://localhost:8010/Basket';

    checkout(payload: CheckoutPayload): Observable<any> {
        const token = localStorage.getItem('token');
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`
        });
        return this.http.post(`${this.baseUrl}/checkout`, payload, {headers});
    }
}