import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { GamesService } from '../_services/games.service';


@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {

  constructor(private gamesService: GamesService, public accountService: AccountService) { }

  games:any = [];

  ngOnInit(): void {
    this.getGames();
  }

  getGames() {
    this.gamesService.getGames().subscribe(games => {
      this.games = games;
      console.log(games);
      this.getHomeTeams();
      this.getAwayTeams();
      this.getWinningTeams();
    }, err => {
      console.log(err);
    })
  }

  getHomeTeams() {
    this.games.forEach(game => {
      return this.gamesService.getTeam(game.homeTeamID).subscribe(team => {
        game.homeTeam = team;
      }, err => {
        console.log(err);
      })
    });
  }

  getAwayTeams() {
    this.games.forEach(game => {
      return this.gamesService.getTeam(game.awayTeamID).subscribe(team => {
        game.awayTeam = team;
      }, err => {
        console.log(err);
      })
    });
  }

  getWinningTeams() {
    this.games.forEach(game => {
      return this.gamesService.getTeam(game.winningTeam).subscribe(team => {
        game.winner = team;
      }, err => {
        console.log(err);
      })
    })
  }



}
