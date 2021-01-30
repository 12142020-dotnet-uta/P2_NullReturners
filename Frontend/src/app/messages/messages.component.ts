import { registerLocaleData } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';
import { MessageService } from '../_services/message.service';
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
  messagesSent:any[] = [];
  messagesRecieved:any[] = [];

  messagesArr: any;

  selectedUserId: string;




  constructor(public accountService: AccountService, private userService: UserService, private messagesService: MessageService) { }

  ngOnInit(): void {
    this.getLoggedInUser();
    this.getUsers();
    this.getMessages();
  }

  // Finds the currently logged in user
  getLoggedInUser() {
    this.accountService.currentUser$.subscribe( user$ => {
      this.userLoggedIn = user$;
    })
  }

  // Gets a list of all users in the app
  // Then creates a list of all players that are on the same team as the logged in user
  getUsers() {
    this.userService.getUsers().subscribe( users => {
      this.usersArr = users;
      this.usersArr.forEach(user => {
        if (user.teamID == this.userLoggedIn.teamID) {
          this.users.push(user)
        }
      })
    })
  }

  // Gets a list of messages
  getMessages() {
    this.messagesService.getMessages().subscribe(messages => {
      this.messagesArr = messages;
      this.getRecipients();
      console.log(messages)
    }, err => {
      console.log(err);
    })
  }

  // gets the recipients from the messages
  getRecipients() {
    this.messagesArr.forEach(message => {
      this.messagesService.getRecipientList(message.recipientListID).subscribe(recipients => {
        message.recipient = recipients;
      }, err => {
        console.log(err);
      })
    });
  }  

  // gets a list of the messages between the two users
  getMessageBox(userId) {
    this.selectedUserId = userId;
    this.messagesSent = [];
    this.messagesRecieved = [];
    this.messagesArr.forEach(message => {
      if (message.senderID === this.userLoggedIn.userID && message.recipient.recipientID === this.selectedUserId) {
        this.messagesSent.push(message);
      } 
      if (message.senderID === this.selectedUserId && message.recipient.recipientID === this.userLoggedIn.userID) {
        this.messagesRecieved.push(message);
        console.log(message.senderID);
      }
    });
  }

}
