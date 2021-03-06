import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { concatMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get(this.baseUrl + 'users');
  }

  getUser(userId) {
    return this.http.get(this.baseUrl + `users/${userId}`)
  }

  getTeams() {
    return this.http.get(this.baseUrl + 'teams');
  }

  getTeam(teamId) {
    return this.http.get(this.baseUrl + `teams/${teamId}`)
  }

  editUser(id:string, model:any) {
    return this.http.put(this.baseUrl + `users/edit/${id}`, model );
  }

  getRoles() {
    return this.http.get(this.baseUrl + 'users/roles');
  }

  getRole(roleId) {
    return this.http.get(this.baseUrl + `users/roles/${roleId}`);
  }

}
