import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserLoggedIn } from '../_models/UserLoggedIn';
import { UserLoggingIn } from '../_models/UserLoggingIn';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: UserLoggingIn = {
    username: null,
    password: null
  };



  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {

  }

  login() {
    this.accountService.login(this.model).subscribe( res => {
      console.log(res);
      this.router.navigate([''])
    }, err => {
      console.log(err);
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigate([''])
  }


}
