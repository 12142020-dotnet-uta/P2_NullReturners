import { registerLocaleData } from '@angular/common';
import { Component, ComponentFactoryResolver, OnInit } from '@angular/core';
import { concatMap, map } from 'rxjs/operators';
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

  
 
  messagesSent:any[] = [];
  messagesRecieved:any[] = [];
  
  message:any = {}

  // need these 
  userLoggedIn:any;
  usersArr: any = {};
  users:User[] = [];
  inboxes:any = [];
  recievedMessagesArr:any[] = [];
  sentMessagesArr:any = [];

  recievedMessages:any[] = [];
  sentMessages:any = [];

  selectedUserId: string;
  allMessages:any = [];

  displayedMessages:any = [];

  constructor(public accountService: AccountService, private userService: UserService, private messagesService: MessageService) { }

  ngOnInit(): void {
    this.getLoggedInUser();
    this.getUsers();
  }

  // Finds the currently logged in user
  getLoggedInUser() {
    this.accountService.currentUser$.subscribe( user$ => {
      this.userLoggedIn = user$;
      this.getInboxes();
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

  getInboxes() {
    this.messagesService.getInboxes(this.userLoggedIn.userID).subscribe(inboxes => {
      this.inboxes = inboxes;
      this.getRecievedMessages();
      this.getSentMessages();
    }, err => {
      console.log(err);
    })
  }

  getRecievedMessages() {
    this.inboxes.forEach(inbox => {
      this.messagesService.getMessage(inbox.messageID).subscribe(message => {
        this.recievedMessagesArr.push(message);
      }, err => {
        console.log(err);
      });
    });
  }

  getSentMessages() {
    this.messagesService.getSentMessages(this.userLoggedIn.userID).subscribe(messages => {
      this.sentMessagesArr = messages;
      this.getRecipients();
    }, err => {
      console.log(err);
    })
  }

  getRecipients() {
    let iterations:number = 0;
    this.sentMessagesArr.forEach(message => {
      this.messagesService.getRecipientList(message.recipientListID).subscribe(recipients => {
        iterations ++;
        message.recipients = recipients;
        if(this.sentMessagesArr.length === iterations) {
          this.getAllMessages();
        }
      }, err => {
        console.log(err);
      });
    });
  }

  getAllMessages() {
    this.allMessages = [];
    this.sentMessagesArr.forEach(message => {
      this.allMessages.push({
        state: 'sent',
        message: message
      });
    });
    this.recievedMessagesArr.forEach(message => {
      this.allMessages.push({
        state: 'recieved',
        message: message
      })
    })
    if (this.selectedUserId) {
      this.getMessageBox(this.selectedUserId);
    }
  }

  // gets a list of the messages between the two users
  getMessageBox(userId) {
    this.selectedUserId = userId;
    this.displayedMessages = [];
    this.allMessages.forEach(message => {
      if (message.message.senderID === this.userLoggedIn.userID && message.message.recipients.recipientID === this.selectedUserId) {
        this.messagesSent.push(message);
        this.displayedMessages.push({
          msg: message,
          state: 'sent',
          date: new Date(message.message.sentDate)
        });
      } 
      if (message.message.senderID === this.selectedUserId) {
        this.messagesRecieved.push(message);
        this.displayedMessages.push({
          msg: message,
          state: 'recieved',
          date: new Date(message.message.sentDate)
        });
      }
    });
    this.displayedMessages.sort((a, b) => a.date - b.date);
  }
   sendMessage() {
    this.message.senderID = this.userLoggedIn.userID;
    this.message.recipientList = [this.selectedUserId];
    this.messagesService.sendMessage(this.message).subscribe( msg => {
      this.message.messageText = '';
      this.getInboxes();
      
    }, err => {
      console.log(err);
    })
  }
}
