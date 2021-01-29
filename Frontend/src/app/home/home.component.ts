import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserLoggedIn } from '../_models/UserLoggedIn';
import { AccountService } from '../_services/account.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public accountService: AccountService, private userService: UserService, private router: Router) { }

  user: UserLoggedIn;
  team:any = {};
  model:any = {};

  ngOnInit(): void {
    if (this.accountService.currentUser$) {
      this.getLoggedInUser();
    }
  }

  getLoggedInUser() {
    this.accountService.currentUser$.subscribe( user => {
      this.user = user;
      if(user) {
        this.getUserTeam();
      }
    });
  }

  getUserTeam() {
    this.userService.getTeam(this.user.teamID).subscribe(team => {
      this.team = team;
    }, err => {
      console.log(err)
    })
  }

  login() {
    this.accountService.login(this.model).subscribe( res => {
      console.log(res);
      this.router.navigate([''])
    }, err => {
      console.log(err);
    });
  }

}
