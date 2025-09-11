import { RouterModule, Routes } from "@angular/router";
import { Login } from "./login/login";
import { Register } from "./register/register";
import { NgModule } from "@angular/core";

const routes: Routes = [
    { path: 'login', component: Login},
    { path: 'register', component: Register}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AuthRoutingModule {}