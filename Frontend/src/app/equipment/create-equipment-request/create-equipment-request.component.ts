import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { EquipmentService } from 'src/app/_services/equipment.service';

@Component({
  selector: 'app-create-equipment-request',
  templateUrl: './create-equipment-request.component.html',
  styleUrls: ['./create-equipment-request.component.css']
})
export class CreateEquipmentRequestComponent implements OnInit {

  itemList: any;
  model: any = {};

  constructor(private equipmentService: EquipmentService, public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.getItemList();
  }

  createEquipmentRequest() {
    this.getCurrentUser();
    this.getCreatedItem();
    this.model.status = 'Requested';
    this.equipmentService.createRequest(this.model).subscribe(response => {
      console.log(response);
      this.router.navigate(['/equipment'])
    }, err => {
      console.log(err);
    })
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe( user => {
      this.model.teamid = user.teamID;
      this.model.userid = user.userID;
    })
  }

  getItemList() {
    this.equipmentService.getItems().subscribe( items => {
      this.itemList = items;
    }, err => {
      console.log(err);
    })
  }

  getCreatedItem() {
    for (let i = 0; i < this.itemList.length; i++) {
      if (this.itemList[i].description == this.model.item) {
        this.model.itemid = this.itemList[i].equipmentID;
      }
    }
  }

}
