import {CanLoad, Route, Router, UrlSegment} from '@angular/router';
import {AuthService} from './auth.service';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanLoad {
    constructor(
        private router: Router,
        private authService: AuthService
    ) {
    }

    canLoad(route: Route, segments: UrlSegment[]):
        Observable<boolean> |
        Promise<boolean> |
        boolean {
        if (route.path === '') {
            console.log('Landing Page Check Guard');
            return true;
        } else if (route.path === 'dashboard') {
            console.log('User Area Check Guard');
            return true;
        } else if (route.path === 'adminDash') {
            console.log('Admin Area Check Guard');
            return true;
        } else {
            return false;
        }
    }
}
