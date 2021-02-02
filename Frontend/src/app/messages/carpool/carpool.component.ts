import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-carpool',
  templateUrl: './carpool.component.html',
  styleUrls: ['./carpool.component.css']
})
export class CarpoolComponent implements OnInit {

  constructor(public accountService: AccountService, private userService: UserService, private messageService: MessageService) { }

  userLoggedIn:any;
  allUsers:any = [];
  allParents:any = [];

  message:any = {};

  ngOnInit(): void {
    this.getLoggedInUser();
    this.getAllUsers();
  }

  getLoggedInUser() {
    this.accountService.currentUser$.subscribe( user$ => {
      this.userLoggedIn = user$;
    })
  }

  getAllUsers() {
    this.userService.getUsers().subscribe(users => {
      this.allUsers = users;
      this.getAllParents();
    }, err => {
      console.log(err);
    })
  }

  getAllParents() {
    this.allUsers.forEach(user => {
      if (user.teamID === this.userLoggedIn.teamID && user.roleID !== 2) {
        this.allParents.push(user);
      }
    });
  }

  sendMessage() {
    this.message.recipientList = [];
    this.allParents.forEach(parent => {
      this.message.recipientList.push(parent.userID);
    });
    this.message.senderID = this.userLoggedIn.userID;
    this.messageService.sendCarpool(this.message).subscribe(msg => {
      console.log(msg);
    }, err => {
      console.log(err);
    });
  }

  getMessages() {

  }

  

}

