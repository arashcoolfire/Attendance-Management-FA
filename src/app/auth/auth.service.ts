import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {take, tap} from 'rxjs/operators';

interface LoginResponse {
    successed: boolean;
    message: string;
    resultObject: {
        personnelId: number,
        firstName: string,
        lastName: string,
        nationalCode: string,
        phoneNo: string,
        email: string,
        fullName: string,
        userId: number,
        password: string,
        userRole: number,
        userRoleName: string,
    };
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(
        private http: HttpClient,
        private router: Router,
    ) {
    }

    async signIn(data: { nationalId: number, password: string }): Promise<boolean> {
        return true;
    }

    async signOut() {
    }

    async signUp(data: {
        nationalId: number,
        firstName: string,
        lastName: string,
    }) {
    }

    async loginAPI(data: { nationalId: string, password: string }): Promise<any> {
        return this.http.get(environment.api.url_https
            + '/Login' +
            '/nationalCode=' + data.nationalId +
            '/passWord=' + data.password).pipe(take(1), tap(res => {
                return res;
            },
            err => {
                return err;
            }));
    }

    async testApi() {
        // return this.http.post(environment.api.url2 +
        //     '/Personnel', {
        //     firstName: 'Mohsen',
        //     lastName: 'Ketabchi',
        //     nationalCode: '0985643536',
        //     phoneNo: '09354801265',
        //     email: 'mohsen123@gmail.com',
        //     userName: 'mohsen',
        //     password: '123',
        //     userRole: 2
        // }).pipe(take(1), tap(res => res));
        return this.http.get(environment.api.url_http);
    }
}
