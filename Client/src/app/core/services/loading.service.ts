import { Injectable, signal } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  // Signal to track loading state
  loading = signal(false);
  show() {
    this.loading.set(true);
  }
  hide() {
    this.loading.set(false);
  }
}