import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DrawService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  createDrawing(model: any) {
    return this.http.post(this.baseUrl + 'playbooks/plays', model);
  }
 
}
