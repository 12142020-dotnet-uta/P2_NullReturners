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

  getGame(id:number) {
    return this.http.get(this.baseUrl + `games/${id}`)
  }

  createGame(game:any) {
    return this.http.post(this.baseUrl + 'games', game);
  }

  editGame(game:any, id:number) {
    return this.http.put(this.baseUrl + `games/edit/${id}`, game);
  }

  getTeams() {
    return this.http.get(this.baseUrl + 'teams');
  }

  getTeam(id:number) {
    return this.http.get(this.baseUrl + `teams/${id}`);
  }

  updateTeam(team:any, id:number) {
    return this.http.put(this.baseUrl + `teams/edit/${id}`, team);
  }

}
