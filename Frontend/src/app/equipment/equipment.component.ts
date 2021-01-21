import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  equipmentList: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEquipment();
  }

  getEquipment() {
    this.http.get('https://localhost:44342/api/equipment').subscribe(response => {
      this.equipmentList = response;
    }), err => {
      console.log(err)
    }
  }

}
