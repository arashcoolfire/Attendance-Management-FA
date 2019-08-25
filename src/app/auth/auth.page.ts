import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {AlertController, LoadingController, ModalController} from '@ionic/angular';
import {timer, interval, Observable} from 'rxjs';
import {map, share, take, tap} from 'rxjs/operators';
import {LoginComponent} from './login/login.component';
import {AuthService} from './auth.service';

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

    constructor(
        private router: Router,
        private loadingCtrl: LoadingController,
        private alertCtrl: AlertController,
        private modalCtr: ModalController,
        private authService: AuthService,
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
        const loader = await this.getLoader();
        loader.present();
        setTimeout(async () => {
            loader.dismiss();
            if (this.enterTo) {
                const alert = await this.getAlert('صبح بخیر، روز کاری خوبی داشته باشید');
                alert.present();
                this.enterTo = !this.enterTo;
            } else if (!this.enterTo) {
                const alert = await this.getAlert('خسته نباشید، بعد از ظهر خوبی داشته باشید');
                alert.present();
                this.enterTo = !this.enterTo;
            }
        }, 540);
    }

    async getLoader() {
        return await this.loadingCtrl.create({message: 'اندکی صبر'});
    }

    async getAlert(message: string) {
        return await this.alertCtrl.create({
            message,
            buttons: [
                {
                    text: 'ممنون',
                    role: 'cancel'
                }
            ]
        });
    }

    async loginUser(data: { nationalId: number, password: string }) {
        const loader = await this.getLoader();
        loader.present();
        // this.authService.loginAPI({nationalId: data.nationalId.toString(), password: data.password})
        //     .then(res => {
        //             console.log(res);
        //         },
        //         err => {
        //             console.log(err);
        //         });
        this.authService.signIn(data).then(res => {
            if (res) {
                setTimeout(async () => {
                    await this.router.navigateByUrl('/dashboard');
                    loader.dismiss();
                }, 540);
            }
        });
    }

    async onTestApi() {
        const req = await this.authService.testApi();
        req.subscribe(res => {
                console.log('res');
                console.log(JSON.stringify(res));
            },
            err => {
                console.log('error');
                console.log(err);
            });
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
