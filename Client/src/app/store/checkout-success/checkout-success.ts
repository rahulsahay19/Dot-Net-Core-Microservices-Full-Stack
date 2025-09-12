import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checkout-success.html',
  styleUrl: './checkout-success.scss'
})
export class CheckoutSuccess implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  orderId: string | null = null;
  totalPrice: number | null = null;

  ngOnInit(): void {
    // read query params passed from checkout
    this.orderId = this.route.snapshot.queryParamMap.get('orderId');
    this.totalPrice = Number(this.route.snapshot.queryParamMap.get('totalPrice'));
  }

  goToOrders() {
    this.router.navigate(['/store/orders']);
  }

  goToStore(){
    this.router.navigate(['/store'])
  }

}
