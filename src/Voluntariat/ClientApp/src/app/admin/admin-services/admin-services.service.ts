import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Service } from './admin-services.models';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})

export class AdminServicesService {

    private API_PATH = '/api/AdminServicesAPI';

    constructor(private http: HttpClient) { }

    public getAll(): Observable<Service[]> {
        return this.http.get<Service[]>(`${this.API_PATH}`);
    }

    public getByID(id: string) {
        return this.http.get(`${this.API_PATH}/${id}`);
    }

    public modifyByID(id: string, service: Service) {
        return this.http.put<Service>(`${this.API_PATH}/${id}`, service);
    }

    public addService(service: Service): Observable<Service> {
        return this.http.post<Service>(this.API_PATH, service);
    }

    public deleteByID(id: string) {
        return this.http.delete(`${this.API_PATH}/${id}`);
    }
}
