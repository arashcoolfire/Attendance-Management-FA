import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

export interface PostPersonnel {
    firstName: string;
    lastName: string;
    nationalCode: string;
    phoneNo: string;
    email: string;
    userName: string;
    password: string;
    userRole: number;
}

export interface PutPersonnel {
    personnelId: number;
    firstName: string;
    lastName: string;
    nationalCode: string;
    phoneNo: string;
    email: string;
    userId: number;
    userName: string;
    password: string;
    userRole: number;
}

@Injectable({
    providedIn: 'root'
})
export class PersonnelService {
    // url = environment.api.url_http + '/Personnel';
    url = environment.api.url_http + '/Personnel';

    constructor(
        private http: HttpClient
    ) {
    }

    async newPersonnel(data: PostPersonnel) {
        return this.http.post(this.url, data);
    }

    async updatePersonnel(data: PutPersonnel) {
        return this.http.put(this.url, data);
    }

    async deletePersonnel(perssonelId: number) {
        return this.http.delete(this.url + '/' + perssonelId);
    }
}
