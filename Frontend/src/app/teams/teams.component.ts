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
    this.http.get('https://localhost:5001/api/teams').subscribe(response => {
      this.teams = response;
    }), err => {
      console.log(err)
    }
  }

}
