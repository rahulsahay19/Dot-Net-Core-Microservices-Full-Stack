import { inject } from "@angular/core"
import { AuthService } from "../auth.service"
import { Router } from "@angular/router";

export const authGuard = () =>{
    const auth = inject(AuthService);
    const router = inject(Router);

    if (auth.isLoggedIn()){
        return true;
    } else {
        //Capture the current URL
        const currentUrl = router.url;
        router.navigate(['/auth/login'], {
            queryParams: {returnUrl: currentUrl}
        });
        return false;
    }
}