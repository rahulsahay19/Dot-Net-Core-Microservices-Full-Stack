import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BasketService } from '../../store/services/basket.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class Navbar implements OnInit {
  searchText = '';
  private basketService = inject(BasketService);
  private authService = inject(AuthService);
  private router = inject(Router);
   
  ngOnInit(): void {
    this.basketService.initializeBasket('rahul.sahay'); 
  }

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

  get loggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  get userName(): string | null {
    return this.authService.getUserName();
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
