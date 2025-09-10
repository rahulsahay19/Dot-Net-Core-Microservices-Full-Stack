import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../services/product.service';
import { Product } from '../models/Product';
import { BasketService } from '../services/basket.service';
import { Basket, BasketItem } from '../models/Basket';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private basketService = inject(BasketService);

  product = signal<Product | null>(null);

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.productService.getProductById(id).subscribe({
          next: (res) => this.product.set(res),
          error: (err) => {
            console.error('Failed to load product', err);
          },
        });
      }
    });
  }

  addToCart(p: Product) {
    const newItem: BasketItem = {
      productId: p.id,
      productName: p.name,
      price: p.price,
      quantity: 1,
      imageFile: p.imageFile,
    };
    // First fetch existing basket
    this.basketService.getBasket('rahul.sahay').subscribe((current) => {
      let items = [...current.items];
      //if product already exists, then increment quantity
      const existing = items.find((i) => i.productId === p.id);
      if (existing) {
        existing.quantity += 1;
      } else {
        items.push(newItem);
      }

      //Recalculate total price
      const basket: Basket = {
        userName: 'rahul.sahay',
        items,
        totalPrice: items.reduce((sum, i) => sum + i.price * i.quantity, 0),
      };

      // Save updated basket
      this.basketService.updateBasket(basket).subscribe({
        next: (res) => {
          this.basketService.setBasket(res);
          console.log('Basket updated', res);
        },
        error: (err) => console.log('Eror adding to basket:', err),
      });
    });
  }
}
