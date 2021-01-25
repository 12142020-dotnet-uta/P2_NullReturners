import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  baseUrl = 'https://localhost:44342/api/'
  constructor(private http: HttpClient) { }

  getRequests() {
    return this.http.get(this.baseUrl + 'equipment');
  }

  getRequest(id) {
    return this.http.get(this.baseUrl + `equipment/${id}`)
  }

  getTeam(teamId) {
    return this.http.get(this.baseUrl + `teams/${teamId}`);
  }

  getUser(userId) {
    return this.http.get(this.baseUrl + `users/${userId}`)
  }

  createRequest(model: any) {
    return this.http.post(this.baseUrl + 'equipment', model);
  }

}
