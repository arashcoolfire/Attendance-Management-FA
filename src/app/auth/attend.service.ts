import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class AttendService {
    constructor(
        private http: HttpClient,
    ) {
    }
    iAmAlive() {
        console.log('Working');
    }
    async attend(nationalId: string) {}
}
