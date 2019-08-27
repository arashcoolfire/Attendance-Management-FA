import {CanLoad, Route, Router, UrlSegment} from '@angular/router';
import {AuthService} from './auth.service';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {take, tap} from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanLoad {
    constructor(
        private router: Router,
        private authService: AuthService,
    ) {
        this.authService.authenticated.subscribe();
    }

    canLoad(route: Route, segments: UrlSegment[]):
        Observable<boolean> |
        Promise<boolean> |
        boolean {
        if (route.path === '') {
            // console.log('Landing Page Check Guard');
            return true;
        } else if (route.path === 'dashboard') {
            return this.authService.authenticated.pipe(take(1), tap(isAuthenticated => {
                if (!isAuthenticated) {
                    this.router.navigateByUrl('/');
                }
                return !!isAuthenticated;
            }));
        } else if (route.path === 'adminDash') {
            console.log('Admin Area Check Guard');
            return true;
        } else {
            return false;
        }
    }
}
