import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {Routes, RouterModule} from '@angular/router';

import {IonicModule} from '@ionic/angular';

import {AuthPage} from './auth.page';
import {LoginComponent} from './login/login.component';
import {DebugerComponent} from './api.debuger/debuger.component';
import {UrlDirective} from './api.debuger/directives/url.directive';
import {AttendService} from './attend.service';

const routes: Routes = [
    {
        path: '',
        component: AuthPage
    }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        RouterModule.forChild(routes),
        ReactiveFormsModule
    ],
    declarations: [AuthPage, LoginComponent, DebugerComponent, UrlDirective],
    entryComponents: [LoginComponent, DebugerComponent],
    providers: [AttendService]
})
export class AuthPageModule {
}
