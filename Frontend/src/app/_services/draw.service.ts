import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DrawService {

  baseUrl = 'https://localhost:44342/api/Playbooks/'
  constructor(private http: HttpClient) { }

  createDrawing(model: any) {
    return this.http.post(this.baseUrl + 'plays', model);
  }
 
  getPlays(){
    return this.http.get(this.baseUrl + 'plays');
  }
  
}
