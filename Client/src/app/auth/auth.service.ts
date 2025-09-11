import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { RegisterDto } from "./models/RegisterDto";
import { Observable } from "rxjs";
import { LoginDto } from "./models/LoginDto";

@Injectable({providedIn:'root'})
export class AuthService {
    private http = inject(HttpClient);
    private baseUrl = 'http://localhost:8010/identity/api/auth';

    //store token in a signal 
    userToken = signal<string | null>(localStorage.getItem('token'));

    register(dto: RegisterDto): Observable<{message: string}>{
        return this.http.post<{message:string}>(`${this.baseUrl}/register`, dto);
    }

    login(dto: LoginDto): Observable<{token: string}>{
        return this.http.post<{token: string}>(`${this.baseUrl}/login`, dto);
    }

    saveToken(token: string){
        localStorage.setItem('token', token);
        this.userToken.set(token);
    }

    logout(){
        localStorage.removeItem('token');
        this.userToken.set(null);
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('token');
    }
}