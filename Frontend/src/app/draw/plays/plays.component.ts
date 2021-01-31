import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DrawService } from 'src/app/_services/draw.service';
import { play } from '../play';

@Component({
  selector: 'app-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.css']
})
export class PlaysComponent implements OnInit {

  constructor(private drawService: DrawService, private route: ActivatedRoute) { }

  myPlay: play;
  play: any;
  model: any = {};
  imageString: string;

  ngOnInit(): void {
    this.getPlays();
  }

  getPlays(){

    this.drawService.getPlays().subscribe(response => {
      this.play = response;
      //this.myPlay = this.play; 
      console.log(response);
    }), err => {
      console.log(err);
    }
  }

  deletePlay(play){
    console.log(play);
    this.drawService.deletePlay(play).subscribe(Response => {
      console.log(Response);
      this.getPlays();
    }), err => {
      console.log(err);
    }
  }


}
