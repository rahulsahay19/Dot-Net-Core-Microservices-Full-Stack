import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Order } from '../models/Order';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders.html',
  styleUrl: './orders.scss',
})
export class Orders implements OnInit {
  private http = inject(HttpClient);
  private authService = inject(AuthService);

  orders = signal<Order[]>([]);
  loading = signal<boolean>(true);

  ngOnInit(): void {
    const userName = 'rahul.sahay'; //TODO: this.authService.getUserName();
    this.http.get<Order[]>(`http://localhost:8010/Order/${userName}`).subscribe({
      next: (res) => {
        const sorted = res.sort((a, b) => b.id - a.id);
        this.orders.set(sorted);
        this.loading.set(false);
      },
      error: (err) => {
        console.error('Failed to load orders', err);
        this.loading.set(false);
      },
    });
  }
}
