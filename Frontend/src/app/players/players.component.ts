import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
  players: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  getPlayers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.players = response;
    }), err => {
      console.log(err)
    }
  }

}
