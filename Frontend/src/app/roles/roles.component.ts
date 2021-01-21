import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  roles: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getRoles();
  }

  getRoles() { 
    this.http.get('https://localhost:5001/api/users/roles').subscribe(response => {
      this.roles = response;
    }), err => {
      console.log(err)
    }
  }
}
