import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
  users: any;
  model: any = {}

  constructor(private http: HttpClient, private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(response => {
          this.users = response;
        }), err => {
          console.log(err)
        }
  }

  createUser() {
    this.userService.createUser(this.model).subscribe(response => {
      console.log(response);
    }), err => {
      console.log(err)
    }
    console.log(this.model);
  }


  // getPlayers() {
  //   this.http.get('https://localhost:44342/api/users').subscribe(response => {
  //     this.players = response;
  //   }), err => {
  //     console.log(err)
  //   }
  // }

}
