import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRequests() {
    return this.http.get(this.baseUrl + 'equipment');
  }

  editRequest(id:string, model:any) {
    return this.http.put(this.baseUrl + `equipment/edit/${id}`, model);
  }

  getRequest(id:number) {
    return this.http.get(this.baseUrl + `equipment/${id}`)
  }

  getTeam(teamId:number) {
    return this.http.get(this.baseUrl + `teams/${teamId}`);
  }

  getUser(userId:string) {
    return this.http.get(this.baseUrl + `users/${userId}`)
  }

  createRequest(model: any) {
    return this.http.post(this.baseUrl + 'equipment', model);
  }

  getItems() {
    return this.http.get(this.baseUrl + `equipment/items`);
  }

  getItem(id:number) {
    return this.http.get(this.baseUrl + `equipment/items/${id}`);
  }

}
