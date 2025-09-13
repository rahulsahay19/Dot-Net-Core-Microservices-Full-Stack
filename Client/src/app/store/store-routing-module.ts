import { RouterModule, Routes } from "@angular/router";
import { Store } from "./store/store";
import { ProductDetails } from "./product-details/product-details";
import { NgModule } from "@angular/core";
import { BasketComponent } from "./basket/basket";
import { CheckoutComponent } from "./checkout/checkout";
import { authGuard } from "../auth/guards/auth.guard";
import { CheckoutSuccess } from "./checkout-success/checkout-success";
import { Orders } from "./orders/orders";

const routes: Routes = [
    { path: '', component: Store }, //deafult
    { path: 'product/:id', component: ProductDetails},
    { path: 'basket', component: BasketComponent}, 
    { path: 'checkout', component: CheckoutComponent, canActivate: [authGuard]},
    { path: 'checkout-success', component: CheckoutSuccess, canActivate: [authGuard]},
    { path: 'orders', component: Orders, canActivate: [authGuard]}
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StoreRoutingModule {}