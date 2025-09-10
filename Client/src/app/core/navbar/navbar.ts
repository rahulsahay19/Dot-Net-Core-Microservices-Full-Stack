import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BasketService } from '../../store/services/basket.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class Navbar {
  searchText = '';
  private basketService = inject(BasketService);
  constructor(private router: Router){}

  get cartCount(){
    return this.basketService.basketCount();
  }

  onSearch() {
    const term = this.searchText.trim();
    if(term) {
      this.router.navigate(['/store'], {queryParams:{search: term}});
    } else {
      this.router.navigate(['/store']); //reset to full catalog
    }
  }
}
