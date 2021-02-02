import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '@auth0/auth0-spa-js';
import { AccountService } from 'src/app/_services/account.service';
import { GamesService } from 'src/app/_services/games.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.css']
})
export class CreateGameComponent implements OnInit {

  constructor(public accountService: AccountService, private gamesService: GamesService, private router: Router) { }

  newGame:any = {};
  teamList:any = [];
  userLoggedIn:any;

  ngOnInit(): void {
    this.getTeamList();
    this.getLoggedInUser();
  }

  createGame() {
    this.getAwayTeam();
    this.getHomeTeam();

    if (this.userLoggedIn.teamID == this.newGame.homeTeamID || this.userLoggedIn.teamID == this.newGame.awayTeamID) {
      this.gamesService.createGame(this.newGame).subscribe(game => {
        console.log(game);
        this.router.navigate(['/games'])
      }, err => {
        console.log(err);
      });
    } else {
      this.teamList.forEach(team => {
        if (team.teamID == this.userLoggedIn.teamID) {
          let teamName = team.name;
          alert(`Created game must include ${teamName}`);
        }
      });
      
    }

    
  }

  getTeamList() {
    this.gamesService.getTeams().subscribe(teams => {
      this.teamList = teams;
      console.log(teams);
    }, err => {
      console.log(err);
    });
  }

  getLoggedInUser() {
    this.accountService.currentUser$.subscribe( user$ => {
      this.userLoggedIn = user$;
    })
  }

  getHomeTeam() {
    for (let i = 0; i < this.teamList.length; i++) {
      if (this.teamList[i].name == this.newGame.hometeam) {
        this.newGame.homeTeamID = this.teamList[i].teamID;
      }
    }
  }

  getAwayTeam() {
    for (let i = 0; i < this.teamList.length; i++) {
      if (this.teamList[i].name == this.newGame.awayteam) {
        this.newGame.awayTeamID = this.teamList[i].teamID;
      }
    }
  }

}
