import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from './admin-categories.models';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})

export class AdminCategoriesService {

    private API_PATH = '/api/AdminCategoriesAPI';

    constructor(private http: HttpClient) { }

    public getAll(): Observable<Category[]> {
        return this.http.get<Category[]>(`${this.API_PATH}`);
    }

    public getByID(id: string) {
        return this.http.get(`${this.API_PATH}/${id}`);
    }

    public modifyByID(id: string, category: Category) {
        return this.http.put<Category>(`${this.API_PATH}/${id}`, category);
    }

    public addCategory(category: Category): Observable<Category> {
        return this.http.post<Category>(this.API_PATH, category);
    }

    public deleteByID(id: string) {
        return this.http.delete(`${this.API_PATH}/${id}`);
    }
}
