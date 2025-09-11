import { Routes } from '@angular/router';
import { Home } from './home/home';
import { ServerError } from './core/server-error/server-error';
import { UnAuthenticated } from './core/un-authenticated/un-authenticated';
import { NotFound } from './core/not-found/not-found';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'store', loadChildren: () => import('./store/store-module').then(m=>m.StoreModule) },
    { path: 'auth', loadChildren: () => import('./auth/auth-module').then(m=>m.AuthModule)},
    { path: 'server-error', component: ServerError },
    { path: 'unauthenticated', component: UnAuthenticated },
    { path: '**', component: NotFound}
];
