import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-create-player',
  templateUrl: './create-player.component.html',
  styleUrls: ['./create-player.component.css']
})
export class CreatePlayerComponent implements OnInit {

  model:any = {};
  teamList:any;
  roleList: any;

  constructor(private accountService: AccountService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getTeamList();
    this.getRoleList();
  }

  createUser() {
    this.getTeam();
    this.getRole();

    this.accountService.registerUser(this.model).subscribe(res => {
      console.log(res)
      this.router.navigate(['/players'])
    }, err => {
      console.log(err);
    });
  }

  getTeamList() {
    this.userService.getTeams().subscribe( teams => {
      this.teamList = teams;
    }, err => {
      console.log(err);
    })
  }

  getTeam() {
    for (let i = 0; i < this.teamList.length; i++) {
      if (this.teamList[i].name == this.model.team) {
        this.model.teamId = this.teamList[i].teamID;
      }
    }
  }

  getRoleList() {
    this.userService.getRoles().subscribe( roles => {
      this.roleList = roles;
    }, err => {
      console.log(err);
    })
  }

  getRole() {
    for (let i = 0; i < this.roleList.length; i++) {
      if (this.roleList[i].roleName == this.model.role) {
        this.model.roleId = this.roleList[i].roleID;
      }
    }
  }

}
