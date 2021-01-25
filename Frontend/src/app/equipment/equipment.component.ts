import { Component, OnInit } from '@angular/core';
import { EquipmentService } from '../_services/equipment.service';


@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  equipmentList: any;
  model: any = {};

  constructor(private equipmentService: EquipmentService) { }

  ngOnInit(): void {
    this.getEquipment();
  }

  getEquipment() {
    this.equipmentService.getRequests().subscribe(response => {
      this.equipmentList = response;
      this.getTeam();
      this.getUser();
    }), err => {
      console.log(err)
    }
  }

  getTeam() {
    this.equipmentList.forEach(element => {
      this.equipmentService.getTeam(element.teamID).subscribe( response => {
        element.team = response;
      }), err => {
        console.log(err);
      };
    });
  }

  getUser() {
    this.equipmentList.forEach(element => {
      this.equipmentService.getUser(element.userID).subscribe( response => {
        element.user = response;
      }), err => {
        console.log(err);
      };
    });
  }

  createEquipmentRequest() {
    this.equipmentService.createRequest(this.model).subscribe(response => {
      console.log(response);
    }, err => {
      console.log(err);
    })
    this.getEquipment();
  }

}
