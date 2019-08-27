import {Component} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AlertController, LoadingController, ModalController} from '@ionic/angular';
import {environment} from '../../../environments/environment';

@Component({
    selector: 'app-debuger',
    templateUrl: './debuger.component.html',
    styleUrls: ['./debuger.component.scss']
})
export class DebugerComponent {
    inputUrl: string;
    testMethod = [
        'POST',
        'GET',
        'PUT',
        'DELETE'
    ];
    selectedMethod: string;
    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    };

    constructor(
        private http: HttpClient,
        private modalCtrl: ModalController,
        private loadingCtr: LoadingController,
        private alertCtr: AlertController,
    ) {
    }

    async getMethodTest() {
        const loader = await this.getLoader();
        const alert = await this.getAlert('Check the browser Debug console');
        let value = {};
        loader.present();
        this.http.get(
            this.inputUrl)
            .subscribe((res: any) => {
                    value = res;
                    console.log(value);
                    console.log('----- GOT THIS RESULT -----');
                    console.log(res);
                    console.log('----- GOT THIS RESULT - STRINGIFY -----');
                    value = JSON.stringify(res);
                    console.log(value);
                    // console.log('----- GOT THIS RESULT - PARSE -----');
                    // console.log(JSON.parse(res));
                    loader.dismiss();
                    alert.present();
                },
                err => {
                    value = err;
                    console.log('----- GOT ERROR -----');
                    console.log(value);
                    // console.log(err);
                    console.log('----- GOT ERROR - STRINGIFY -----');
                    value = JSON.stringify(err);
                    console.log(value);
                    // console.log('----- GOT ERROR - PARSE -----');
                    // console.log(JSON.parse(err));
                    loader.dismiss();
                    alert.present();
                });
    }

    async testValues() {
        const body = {
            firstName: 'Mohsen',
            lastName: 'Ketabchi',
            nationalCode: '0985643536',
            phoneNo: '09354801265',
            email: 'mohsen123@gmail.com',
            userName: 'mohsen',
            password: '123',
            userRole: 2
        };
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };

        await this.http.post(
            `http://localhost:5000/api/Personnel`,
            body, httpOptions)
            .subscribe(
                (res: any) => {
                    console.log(res);
                }, err => {
                    console.log(err);
                });
    }

    async sendGetWithHeaders() {
        this.http.get(
            'http://localhost:5000/api/Personnel',
            this.httpOptions).subscribe(
            (res: any) => {
                console.log(res);
            }, (err: any) => {
                console.log(err);
            }
        );
    }

    async attendTest() {
        this.http.post(environment.api.url_http + '/Attendance/AddTimeByNationalCode',
            {NationalCode: '4444455555', TimeType: 3}, this.httpOptions).subscribe(
            (res: any) => {
                console.log(res);
            }, (err: any) => {
                console.log(err);
            }
        );
    }

    onStart() {
        // this.testValues();
        // this.sendGetWithHeaders();
        this.attendTest();
        // if (this.selectedMethod === 'GET') {
        //     if (this.inputUrl) {
        //         this.getMethodTest();
        //     }
        // }
    }

    onExit() {
        this.modalCtrl.dismiss();
    }

    async getLoader() {
        return await this.loadingCtr.create({
            message: 'Wait...'
        });
    }

    async getAlert(message?: string) {
        return await this.alertCtr.create({
            message,
            buttons: [{
                text: 'OK',
                role: 'cancel'
            }]
        });
    }
}
