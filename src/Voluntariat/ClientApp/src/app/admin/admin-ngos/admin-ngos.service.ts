import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NGO } from './admin-ngos.models';
import { Observable } from 'rxjs/internal/Observable';
@Injectable({
  providedIn: 'root'
})
export class AdminNgosService {
  private API_PATH = '/api/AdminNGOsAPI';
  //formData: NGO;
  //list: NGO[];

  constructor(private http: HttpClient) { }

  public getAll(): Observable<NGO> {
    return this.http.get<NGO>(`${this.API_PATH}`);
  }

  //public getByID(): Observable<NGO> {
  //  return this.http.get<NGO>(`${this.API_PATH}`);
  //}
}
