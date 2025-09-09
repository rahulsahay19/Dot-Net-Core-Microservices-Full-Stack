import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-store',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './store.html',
  styleUrl: './store.scss',
})
export class Store {
  private route = inject(ActivatedRoute);

  //Mock Data
  products = signal(
    Array.from({ length: 50 }).map((_, i) => ({
      id: i + 1,
      name: `Product ${i + 1}`,
      brand: i % 2 === 0 ? 'Nike' : 'Adidas',
      type: i % 3 === 0 ? 'Shoes' : 'Clothing',
      price: (i + 1) * 100,
      image: `https://via.placeholder.com/200*200?text=Product+${i + 1}`,
    })),
  );
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
    this.route.queryParams.subscribe((params) => {
      this.searchTerm.set(params['search'] || '');
      this.currentPage.set(1); //reset pagination on new search
    });
  }
  // Computed filtered products
  filteredProducts = computed(() => {
    let result = this.products();

    //Apply search filter
    if (this.searchTerm()) {
      result = result.filter((p) => p.name.toLowerCase().includes(this.searchTerm().toLowerCase()));
    }

    // Brand Filter
    if (this.selectedBrand()) {
      result = result.filter((p) => p.brand === this.selectedBrand());
    }

    // Type Filter
    if (this.selectedType()) {
      result = result.filter((p) => p.type === this.selectedType());
    }

    // Sorting
    if (this.sortOption() == 'priceAsc') {
      result = [...result].sort((a, b) => a.price - b.price);
    } else if (this.sortOption() == 'priceDesc') {
      result = [...result].sort((a, b) => b.price - a.price);
    }
    return result;
  });

  // Reset filters
  resetFilters() {
    this.searchTerm.set('');
    this.selectedBrand.set(null);
    this.selectedType.set(null);
    this.sortOption.set('default');
    this.currentPage.set(1);
  }

  paginatedProducts = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize;
    return this.filteredProducts().slice(start, start + this.pageSize);
  });

  totalPages = computed(() => Math.ceil(this.filteredProducts().length / this.pageSize));

  // Pagination controls
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
    }
  }
}
