import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { concatMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:44342/api/'
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get(this.baseUrl + 'users');
  }

  getUser(userId) {
    return this.http.get(this.baseUrl + `users/${userId}`)
  }

  getTeam(teamId) {
    return this.http.get(this.baseUrl + `teams/${teamId}`)
  }

  createUser(model: any) {
    return this.http.post(this.baseUrl + 'users', model);
  }

  editUser(id:string, model:any) {
    return this.http.put(this.baseUrl + `users/edit/${id}`, model );
  }
}
