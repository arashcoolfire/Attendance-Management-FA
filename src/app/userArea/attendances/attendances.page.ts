import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {LoadingController} from '@ionic/angular';

@Component({
    selector: 'app-tab1',
    templateUrl: 'attendances.page.html',
    styleUrls: ['attendances.page.scss']
})
export class AttendancesPage {

    constructor(
        private router: Router,
        private loadingCtrl: LoadingController,
    ) {
    }

    async onExit() {
        const loader = await this.getLoding();
        loader.present();
        setTimeout(() => {
            this.router.navigateByUrl('');
            loader.dismiss();
        }, 540);
    }

    async getLoding() {
        return await this.loadingCtrl.create({message: 'اندکی صبر'});
    }

}
