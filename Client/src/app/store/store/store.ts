import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-store',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './store.html',
  styleUrl: './store.scss'
})
export class Store {
  private route = inject(ActivatedRoute);

  //Mock Data
  products = signal(Array.from({length: 50}).map((_, i) =>({
    id: i + 1,
    name: `Product ${i + 1}`,
    brand: i % 2 === 0 ? 'Nike': 'Adidas',
    type: i % 3 === 0 ? 'Shoes': 'Clothing',
    price: (i + 1) * 100,
    image: `https://via.placeholder.com/200*200?text=Product+${i + 1}`
  })));
  //Global seach term
  searchTerm = signal('');

  //Filters
  selectedBrand = signal<string | null>(null);
  selectedType = signal<string | null>(null);
  sortOption = signal('default');

  //Pagination 
  pageSize = 10;
  currentPage = signal(1);

  constructor() {
    // watch for search query param
    this.route.queryParams.subscribe(params => {
      this.searchTerm.set(params['search'] || '');
      this.currentPage.set(1); //reset pagination on new search
    });
  }
}
