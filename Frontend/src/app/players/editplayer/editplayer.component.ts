import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-editplayer',
  templateUrl: './editplayer.component.html',
  styleUrls: ['./editplayer.component.css']
})
export class EditplayerComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private titleService: Title) { }

  userId: string;
  user: any = {};
  editedUser: any = {};


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params.id;
    });

    this.getUser(this.userId);
  }

  getUser(userId) {
    this.userService.getUser(userId).subscribe(response => {
      this.user = response;
      this.titleService.setTitle(`Edit - ${this.user.userName}`);

      this.editedUser = {
        fullname: this.user.fullName,
        password: this.user.password,
        phonenumber: this.user.phoneNumber,
        email: this.user.email
      };
    }), err => {
      console.log(err);
    }
  }

  editUser() {
    console.log(this.editedUser);
    this.userService.editUser(this.userId, this.editedUser).subscribe(res => {
      console.log(res);
      this.editedUser = {};

      // make this a redirect later
      this.getUser(this.userId);
    }), err => {
      console.log(err);
    }
  }


}
