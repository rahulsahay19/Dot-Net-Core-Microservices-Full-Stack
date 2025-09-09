import { RouterModule, Routes } from "@angular/router";
import { Store } from "./store/store";
import { ProductDetails } from "./product-details/product-details";
import { NgModule } from "@angular/core";

const routes: Routes = [
    { path: '', component: Store }, //deafult
    { path: 'product/:id', component: ProductDetails}
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StoreRoutingModule {}