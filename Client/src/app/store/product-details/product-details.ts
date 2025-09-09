import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../services/product.service';
import { Product } from '../models/Product';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss'
})
export class ProductDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);

  product = signal<Product | null>(null);

    ngOnInit(): void {
      this.route.paramMap.subscribe(params =>{
        const id = params.get('id');
        if(id){
          this.productService.getProductById(id).subscribe({
            next: res => this.product.set(res),
            error: err => {
              console.error('Failed to load product', err);
            } 
          });
        }
      })
  }
}
