import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NGO } from './admin-ngos.models';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
    providedIn: 'root'
})

export class AdminNgosService {
    private API_PATH = '/api/AdminNGOsAPI';

    constructor(private http: HttpClient) { }

    public getAll(): Observable<NGO[]> {
        return this.http.get<NGO[]>(`${this.API_PATH}`);
    }

    public getByID(id: string) {
        return this.http.get(`${this.API_PATH}/${id}`);
    }

    public verifyByID(ngo: NGO): Observable<NGO> {
        return this.http.post<NGO>(this.API_PATH, ngo);
    }

    public deleteByID(id: string) {
        return this.http.delete(`${this.API_PATH}/${id}`);
    }
}
