import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {AlertController, LoadingController, ModalController} from '@ionic/angular';
import {timer, interval, Observable} from 'rxjs';
import {map, share, take, tap} from 'rxjs/operators';
import {LoginComponent} from './login/login.component';
import {AuthService} from './auth.service';
import {DebugerComponent} from './api.debuger/debuger.component';
import {tryCatch} from 'rxjs/internal-compatibility';
import {AttendService} from './attend.service';

@Component({
    selector: 'app-auth',
    templateUrl: './auth.page.html',
    styleUrls: ['./auth.page.scss'],
})
export class AuthPage implements OnInit {
    enterLink = '/dashboard';
    enterTo = true;
    date = new Date();
    hour = this.date.getHours();
    min = this.date.getMinutes();
    sec = this.date.getSeconds();
    clock: Observable<Date>;

    nationalObject = {
        inputNational: null,
        errorMessage: 'کد ملی نامعتبر',
        hasError: false,
    };


    constructor(
        private router: Router,
        private loadingCtrl: LoadingController,
        private alertCtrl: AlertController,
        private modalCtr: ModalController,
        private authService: AuthService,
        private attendService: AttendService,
    ) {
        this.oberserableTimer();
        this.clock.subscribe(val => {
            this.updateClock(val);
        });
    }

    ngOnInit() {
    }

    async onLogInClick() {
        const loader = await this.getLoader();
        const modal = await this.modalCtr.create({component: LoginComponent});
        loader.present();
        setTimeout(() => {
            modal.present();
            loader.dismiss();
        }, 540);
        modal.onDidDismiss()
            .then((data: any) => {
                if (data.data.nId && data.data.pasW) {
                    this.loginUser({nationalId: data.data.nId, password: data.data.pasW});
                }
            });
    }

    async onSubmitClick() {
        if (
            this.nationalObject.inputNational !== null &&
            typeof Number(this.nationalObject.inputNational) === 'number' &&
            !isNaN(Number(this.nationalObject.inputNational)) &&
            this.nationalObject.inputNational.length === 10
        ) {
            this.nationalObject.hasError = false;
            this.nationalObject.inputNational = null;
            const loader = await this.getLoader();
            loader.present();
            loader.dismiss();
            if (this.enterTo) {
                const alert = await this.getAlert('صبح بخیر، روز کاری خوبی داشته باشید');
                this.attendService.iAmAlive();
                alert.present();
                this.enterTo = !this.enterTo;
            } else if (!this.enterTo) {
                const alert = await this.getAlert('خسته نباشید، بعد از ظهر خوبی داشته باشید');
                alert.present();
                this.enterTo = !this.enterTo;
            }
        } else {
            this.nationalObject.hasError = true;
            this.nationalObject.inputNational = null;
        }
    }

    async getLoader() {
        return await this.loadingCtrl.create({message: 'اندکی صبر'});
    }

    async getAlert(message: string, btnTxt?: string) {
        return await this.alertCtrl.create({
            message,
            buttons: [
                {
                    text: btnTxt || 'ممنون',
                    role: 'cancel'
                }
            ]
        });
    }

    async loginUser(data: { nationalId: number, password: string }) {
        const loader = await this.getLoader();
        loader.present();
        let login;
        try {
            login = await this.authService.loginAPI({nationalId: data.nationalId.toString(), password: data.password});

        } catch (e) {
            console.log(e);
        }
        login.subscribe(
            async (res: any) => {
                loader.dismiss();
            }, async (err: any) => {
                loader.dismiss();
                const alert = await this.getAlert('ورود ناموفق', 'بازتلاش');
                alert.present();
            }
        );
    }

    async onTestApi() {
        const modal = await this.modalCtr.create({component: DebugerComponent});
        modal.present();
    }

    oberserableTimer() {
        this.clock = interval(1000).pipe(
            map(tick => new Date()),
            share()
        );
    }

    updateClock(time: Date) {
        this.hour = time.getHours();
        this.min = time.getMinutes();
        this.sec = time.getSeconds();
    }

}
