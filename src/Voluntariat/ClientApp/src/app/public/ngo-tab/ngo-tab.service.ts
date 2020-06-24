import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PublicNGO } from './ngo-tab.models';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
    providedIn: 'root'
})

export class NGOTabService {
    private API_PATH = '/api/ApiNgo';

    constructor(private http: HttpClient) { }

    public getAll(): Observable<PublicNGO[]> {
        return this.http.get<PublicNGO[]>(`${this.API_PATH}`);
    }
}
