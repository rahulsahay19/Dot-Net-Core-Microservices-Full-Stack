import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BasketService } from '../services/basket.service';
import { Basket, BasketItem } from '../models/Basket';


@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.html',
  styleUrls: ['./basket.scss']
})
export class BasketComponent implements OnInit {
  private basketService = inject(BasketService);

  basket = signal<Basket | null>(null);

  ngOnInit() {
    // Load basket from localStorage first
    const local = this.basketService.basket;
    if (local) {
      this.basket.set(local);
    }

    // Optionally: refresh from backend to sync latest
    this.basketService.getBasket('rahul.sahay').subscribe({
      next: (res) => {
        this.basket.set(res);
        this.basketService.setBasket(res); // sync localStorage + badge
      },
      error: (err) => console.error('Error loading basket:', err)
    });
  }

  increment(item: BasketItem) {
    item.quantity++;
    this.updateBasket();
  }

  decrement(item: BasketItem) {
    if (item.quantity > 1) {
      item.quantity--;
      this.updateBasket();
    } else {
      this.removeItem(item);
    }
  }

  removeItem(item: BasketItem) {
    const updatedItems = this.basket()?.items.filter(i => i.productId !== item.productId) || [];
    this.basket.set({
      userName: 'rahul.sahay',
      items: updatedItems,
      totalPrice: updatedItems.reduce((sum, i) => sum + i.price * i.quantity, 0)
    });
    this.updateBasket();
  }

  private updateBasket() {
    const updated = this.basket();
    if (updated) {
      updated.totalPrice = updated.items.reduce((sum, i) => sum + i.price * i.quantity, 0);

      this.basketService.updateBasket(updated).subscribe({
        next: (res) => this.basketService.setBasket(res),
        error: (err) => console.error('Error updating basket:', err)
      });
    }
  }

}
