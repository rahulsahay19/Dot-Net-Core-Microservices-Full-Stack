import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../services/product.service';
import { Product } from '../models/Product';
import { Brand } from '../models/Brand';
import { Type } from '../models/Type';

@Component({
  selector: 'app-store',
  imports: [CommonModule, RouterModule],
  standalone: true,
  templateUrl: './store.html',
  styleUrl: './store.scss',
})
export class Store implements OnInit {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);

  //Signals
  products = signal<Product[]>([]);
  totalCount = signal(0);
  brands = signal<Brand[]>([]);
  types = signal<Type[]>([]);
  searchTerm = signal<string>('');

  //Filters
  selectedBrand = signal<string | null>(null);
  selectedType = signal<string | null>(null);
  sortOption = signal('default');

  //Pagination
  pageSize = 10;
  currentPage = signal(1);

  ngOnInit(): void {
    this.loadProducts();
    this.loadBrands();
    this.loadTypes();
     // watch for search query param
    this.route.queryParams.subscribe((params) => {
      this.searchTerm.set(params['search'] || '');
      this.currentPage.set(1); //reset pagination on new search
      this.loadProducts();
    });
  }

  loadProducts() {
    this.productService.getAllProducts(
      this.currentPage(),
      this.pageSize,
      this.selectedBrand(),
      this.selectedType(),
      this.sortOption(),
      this.searchTerm()
    ).subscribe(res =>{
      this.products.set(res.data);
      this.totalCount.set(res.count);
    })
  }

  loadBrands() {
    this.productService.getAllBrands().subscribe(res=> this.brands.set(res));
  }

  loadTypes() {
    this.productService.getAllTypes().subscribe(res => this.types.set(res));
  }

  //Apply filters
  applyFilters() {
    this.currentPage.set(1); // reset pagination
    this.loadProducts();
  }
  // Computed filtered products
  filteredProducts = computed(() => {
    let result = this.products();

    //Apply search filter
     // Brand Filter
    if (this.selectedBrand()) {
      result = result.filter(p => p.brand.name === this.selectedBrand());
    }

    // Type Filter
    if (this.selectedType()) {
      result = result.filter(p => p.type.name === this.selectedType());
    }
    
    if (this.searchTerm()) {
      const term = this.searchTerm().toLowerCase();
      result = result.filter(
        p => 
          p.name.toLowerCase().includes(term) ||
          p.brand.name.toLowerCase().includes(term) ||
          p.type.name.toLowerCase().includes(term) ||
          (p.description && p.description.toLowerCase().includes(term))
      )
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
    this.loadProducts();
  }

  paginatedProducts = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize;
    return this.filteredProducts().slice(start, start + this.pageSize);
  });

  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize));

  // Pagination controls
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
      this.loadProducts();
    }
  }
}
