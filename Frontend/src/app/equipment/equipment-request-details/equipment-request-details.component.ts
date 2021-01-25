import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EquipmentService } from 'src/app/_services/equipment.service';

@Component({
  selector: 'app-equipment-request-details',
  templateUrl: './equipment-request-details.component.html',
  styleUrls: ['./equipment-request-details.component.css']
})
export class EquipmentRequestDetailsComponent implements OnInit {

  constructor(private equipmentService: EquipmentService, private route: ActivatedRoute ) { }
  equipmentRequestId: string;
  equipmentRequest: any = {};

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      console.log(params)
      this.equipmentRequestId = params.id;
    });

    this.getRequest(this.equipmentRequestId);
  }

  getRequest(id) {
    this.equipmentService.getRequest(id).subscribe(res => {
      this.equipmentRequest = res;
    }), err => {
      console.log(err);
    }
  }

}
