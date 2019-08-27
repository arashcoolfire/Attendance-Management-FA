import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {LoadingController} from '@ionic/angular';
import {AuthService} from '../../auth/auth.service';

@Component({
    selector: 'app-tab1',
    templateUrl: 'attendances.page.html',
    styleUrls: ['attendances.page.scss']
})
export class AttendancesPage {

    constructor(
        private router: Router,
        private loadingCtrl: LoadingController,
        private authService: AuthService,
    ) {
    }

    async onExit() {
        const loader = await this.getLoding();
        loader.present();
        this.authService.signOut().then(ok => {
            loader.dismiss();
        });
    }

    async getLoding() {
        return await this.loadingCtrl.create({message: 'اندکی صبر'});
    }

}
