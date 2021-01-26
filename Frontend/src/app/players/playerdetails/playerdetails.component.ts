import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { UserService } from 'src/app/_services/user.service';


@Component({
  selector: 'app-playerdetails',
  templateUrl: './playerdetails.component.html',
  styleUrls: ['./playerdetails.component.css']
})
export class PlayerdetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  user: any = {};
  userId: string;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params.id;
    });
    this.getUser(this.userId);
  }

  getUser(userId) {
    this.userService.getUser(userId).subscribe(response => {
      this.user = response;
      this.getTeam();
    }), err => {
      console.log(err);
    }
  }

  getTeam() {
      this.userService.getTeam(this.user.teamID).subscribe( response => {
        this.user.team = response;
      }), err => {
        console.log(err);
      };
  }



  
}
