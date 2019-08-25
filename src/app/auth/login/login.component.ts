import {Component} from '@angular/core';
import {ModalController} from '@ionic/angular';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-login-form',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    loginForm = new FormGroup({
        nationalId: new FormControl(null,
            [
                Validators.required,
                Validators.minLength(10)
            ]),
        password: new FormControl(null,
            [
                Validators.required,
                Validators.minLength(6)
            ])
    });
    constructor(
        private modalCtrl: ModalController
    ) {}
    async onSubmit() {
        if (this.loginForm.valid) {
            this.modalCtrl.dismiss({
                nId: this.loginForm.controls.nationalId.value,
                pasW: this.loginForm.controls.password.value
            });
        }
    }
    async onCancel() {
        this.modalCtrl.dismiss({dismiss: true});
    }
}
