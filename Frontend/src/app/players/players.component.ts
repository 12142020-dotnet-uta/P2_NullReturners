import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { concatMap, mergeAll, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})



export class PlayersComponent implements OnInit {
  users: any;
  model: any = {}


  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(response => {
          this.users = response;
          this.getTeams();
        }), err => {
          console.log(err);
        }
  }


  getTeams() {
    this.users.forEach(element => {
      this.userService.getTeam(element.teamID).subscribe( response => {
        element.team = response;
      }), err => {
        console.log(err);
      };
    });
  }

  createUser() {
    this.userService.createUser(this.model).subscribe(response => {
      console.log(response);
    }), err => {
      console.log(err)
    }
    this.getUsers();
  }

}
