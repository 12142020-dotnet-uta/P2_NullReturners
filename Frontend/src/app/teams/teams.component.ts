import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
  
  teams: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(){
    this.http.get('https://localhost:44342/api/teams').subscribe(response => {
      this.teams = response;
      this.calculatePCT();
    }), err => {
      console.log(err)
    }
  }

  calculatePCT() {
    this.teams.forEach(team => {
      if(team.wins || team.losses) {
        team.winningPct = (team.wins / (team.wins + team.losses)).toPrecision(2);
      } 
    });
  }

}
