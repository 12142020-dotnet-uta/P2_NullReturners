import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl;

  getGames() {
    return this.http.get(this.baseUrl + 'games')
  }

  createGame(game:any) {
    return this.http.post(this.baseUrl + 'games', game);
  }

  getTeams() {
    return this.http.get(this.baseUrl + 'teams');
  }

  getTeam(id:number) {
    return this.http.get(this.baseUrl + `teams/${id}`);
  }
}
