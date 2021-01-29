import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  userLoggedIn:any;
  users:User[] = [];
  usersArr: any = {};


  constructor(public accountService: AccountService, private userService: UserService) { }

  ngOnInit(): void {
    this.getUser();
    this.getUsers()
  }

  getUser() {
    this.accountService.currentUser$.subscribe( user$ => {
      this.userLoggedIn = user$;
    })
  }

  getUsers() {
    this.userService.getUsers().subscribe( users => {
      this.usersArr = users;
      this.usersArr.forEach(user => {
        if (user.teamID == this.userLoggedIn.teamID) {
          this.users.push(user)
        }
      })
      console.log(this.users);
    })
  }

}
