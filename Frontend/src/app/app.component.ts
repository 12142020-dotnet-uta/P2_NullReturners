import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';
  users: any;
  // user: any

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getUsers();
    // this.getUser();
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }), err => {
      console.log(err)
    }
  }

  // getUser() {
  //   this.http.get('https://localhost:5001/api/users/76026bc7-63e9-4695-8985-0144f059e813').subscribe(response => {
  //     this.user = response;
  //   }), err => {
  //     console.log(err)
  //   }
  // }

}
