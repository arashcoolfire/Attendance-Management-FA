import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Injectable, OnInit} from '@angular/core';
import {environment} from '../../environments/environment';
import {take, tap} from 'rxjs/operators';
import {Plugins} from '@capacitor/core';
import {BehaviorSubject} from 'rxjs';
import {tryCatch} from 'rxjs/internal-compatibility';

const {Storage} = Plugins;

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
export class AuthService implements OnInit {
    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    };
    private _AUTHENTICATED = new BehaviorSubject<boolean>(false);

    constructor(
        private http: HttpClient,
        private router: Router,
    ) {
        this.autoLogin();
    }

    ngOnInit(): void {
        this.autoLogin();
    }

    get authenticated() {
        return this._AUTHENTICATED.asObservable();
    }

    // async signIn(data: { nationalId: number, password: string }): Promise<boolean> {
    //     return true;
    // }

    async signOut() {
        await Storage.clear();
        this._AUTHENTICATED.next(false);
        this.router.navigateByUrl('/');
    }

    async signUp(data: {
        nationalId: number,
        firstName: string,
        lastName: string,
    }) {
    }

    async loginAPI(data: { nationalId: string, password: string }): Promise<any> {
        return await this.http.get(environment.api.url_http
            + '/Login' +
            '/' + data.nationalId +
            '/' + data.password, this.httpOptions)
            .pipe(
                take(1),
                tap(
                    async (result: any) => {
                        if (result.successed) {
                            await this.storeDataWithExpireDate(result.resultObject);
                            this._AUTHENTICATED.next(true);
                            this.router.navigateByUrl('/dashboard');
                        }
                    }, err => {
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

    async storeDataWithExpireDate(data: any) {
        const toDay = new Date();
        const expireAt = new Date();
        expireAt.setMinutes(toDay.getMinutes() + 30);
        const storedDetails = {
            data,
            expireAt,
        };
        const dataStr = await JSON.stringify(storedDetails);
        await Storage.set({key: 'data', value: dataStr});
    }

    async autoLogin() {
        const dataStr = await Storage.get({key: 'data'});
        if (dataStr !== null || !undefined) {
            const dataJson = JSON.parse(dataStr.value);

            if (dataJson !== null) {
                if (new Date(dataJson.expireAt) >= new Date()) {
                    const login = await this.loginAPI({nationalId: dataJson.data.nationalCode, password: dataJson.data.password});
                    login.subscribe();
                } else if (!(new Date(dataJson.expireAt) <= new Date())) {
                    await this.signOut();
                }
            }
        }
    }
}
