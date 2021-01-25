import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-editplayer',
  templateUrl: './editplayer.component.html',
  styleUrls: ['./editplayer.component.css']
})
export class EditplayerComponent implements OnInit {

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  user: any = {};
  userId: string;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      console.log(params)
      this.userId = params.id;
    });
    console.log(this.userId);

    this.getUser(this.userId);
  }

  getUser(userId) {
    this.userService.getUser(userId).subscribe(response => {
      this.user = response;
    }), err => {
      console.log(err);
    }
  }


}
