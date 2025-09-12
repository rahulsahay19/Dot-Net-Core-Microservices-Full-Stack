import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BasketService } from '../services/basket.service';
import { AuthService } from '../../auth/auth.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CheckoutService } from '../services/checkout.service';
import { CheckoutPayload } from '../models/CheckoutPayload';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.scss']
})
export class CheckoutComponent {
  private fb = inject(FormBuilder);
  private basketService = inject(BasketService);
  private router = inject(Router);
  private checkoutService = inject(CheckoutService);
  private authService = inject(AuthService);
  private snackBar = inject(MatSnackBar);

  checkoutForm = this.fb.group({
    //TODO: Get basket name as default value as username
    //userName: [{ value: this.authService.getUserName(), disabled: true }, Validators.required],
    userName: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    emailAddress: ['', [Validators.required, Validators.email]],
    addressLine: ['', Validators.required],
    country: ['', Validators.required],
    state: ['', Validators.required],
    zipCode: ['', Validators.required],
    cardName: ['', Validators.required],
    cardNumber: ['', [Validators.required, Validators.minLength(16)]],
    expiration: ['', Validators.required],
    cvv: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(4)]],
    paymentMethod: [1, Validators.required]
  });

  get basket() {
    return this.basketService.basket;
  }

  onSubmit() {
    if (this.checkoutForm.invalid) return;

    const payload: CheckoutPayload = {
      ...this.checkoutForm.getRawValue(), 
      totalPrice: this.basket?.totalPrice ?? 0
    } as CheckoutPayload;

    this.checkoutService.checkout(payload).subscribe({
    next: (orderId: number) => {
      this.basketService.clearBasket();

      this.snackBar.open('Order placed successfully 🎉', 'Close', {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: ['success-snackbar']
      });

      setTimeout(() => {
          this.router.navigate(['/store/checkout-success'], {
            queryParams: { orderId, totalPrice: payload.totalPrice }
          });
        }, 2000);
      },
      error: () => {
        this.snackBar.open('Checkout failed ❌', 'Close', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'top',
          panelClass: ['error-snackbar']
        });
      }
    });

  }
}
