import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DrawService } from 'src/app/_services/draw.service';

@Component({
  selector: 'app-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.css']
})
export class PlaysComponent implements OnInit {

  constructor(private drawService: DrawService, private route: ActivatedRoute) { }

  play: any;
  model: any = {};
  imageString: string;

  ngOnInit(): void {
    this.getPlays();
  }

  getPlays(){

    this.drawService.getPlays().subscribe(response => {
      this.play = response;
      console.log(response);
    }), err => {
      console.log(err);
    }
  }

  convertImage(){
    
  }
}
