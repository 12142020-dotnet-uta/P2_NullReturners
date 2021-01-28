
import {
  Component, Input, ElementRef, AfterViewInit,OnInit, ViewChild
} from '@angular/core';
import { NgModel } from '@angular/forms';
import { fromEvent } from 'rxjs';
import { switchMap, takeUntil, pairwise } from 'rxjs/operators'
import { PlayerdetailsComponent } from '../players/playerdetails/playerdetails.component';
import { DrawService } from '../_services/draw.service';
import { UserService } from '../_services/user.service';
import { play } from './play';

@Component({
  selector: 'app-draw',
  templateUrl: './draw.component.html',
  styles: ['canvas { border: 1px solid; }']
})
export class DrawComponent implements AfterViewInit {
  // a reference to the canvas element from our template
  @ViewChild('canvas') canvas: any;
  
  // setting a width and height for the canvas
  @Input() public width = 600;
  @Input() public height = 600;
  constructor(private drawService: DrawService, private userService: UserService){}
  model = new play;
  
  ImageString;
  canvasEl: HTMLCanvasElement;
  cx: CanvasRenderingContext2D;

  

  public ngAfterViewInit() {
    // get the context
    this.canvasEl  = this.canvas.nativeElement;
    this.cx = this.canvasEl.getContext('2d');
    // set the width and height
    this.canvasEl.width = this.width;
    this.canvasEl.height = this.height;
    this.cx.lineCap = 'round';
  }

  
public captureEvents() {
  // this will capture all mousedown events from the canvas element
  fromEvent(this.canvasEl, 'mousedown')
    .pipe(
       switchMap((e) =>  {
        // after a mouse down, we'll record all mouse moves
        return fromEvent(this.canvasEl, 'mousemove')
          .pipe(
            // we'll stop once the user releases the mouse
            // triggers a mouseup event    
            takeUntil(fromEvent(this.canvasEl, 'mouseup')),
            //stop and unsubscribe once the (mouseleave event)
            takeUntil(fromEvent(this.canvasEl, 'mouseleave')),
            // pairwise lets us get the previous value to draw a line from the previous point to the current point    
            pairwise()
          )
      })
    )
    .subscribe((res: [MouseEvent, MouseEvent])  => {
      const rect = this.canvasEl.getBoundingClientRect();

      // previous and current position with the offset
      const prevPos = {
        x: res[0].clientX - rect.left,
        y: res[0].clientY - rect.top
      };

      const currentPos = {
        x: res[1].clientX - rect.left,
        y: res[1].clientY - rect.top
      };

      // this method we'll implement soon to do the actual drawing
      this.drawOnCanvas(prevPos, currentPos);
    });
}

private drawOnCanvas( lastPosition:{ x: number, y: number }, positionNow: { x: number, y: number }) {
  // incase the context is not set
  if (!this.cx) { return; }

  // start our drawing path
  this.cx.beginPath();

  // we're drawing lines so we need a previous position
  if (lastPosition) { // sets the start point
    this.cx.moveTo(lastPosition.x, lastPosition.y); // from
    this.cx.lineTo(positionNow.x, positionNow.y);// draws a line from the start pos until the current position
    this.cx.stroke(); // strokes the current path with the styles we set earlier
  }
}

public getRed(){
  this.cx.strokeStyle = 'Red';
}

public getBlack(){
  this.cx.strokeStyle = 'Black';
}

public getWhite(){
  this.cx.strokeStyle = 'White';
}

public getBlue(){
  this.cx.strokeStyle = 'Blue';
}

public getEraser(){
  this.cx.strokeStyle = this.canvasEl.style.backgroundColor;
}

public lineIncrease(){
  if(this.cx.lineWidth < 40){
  this.cx.lineWidth = this.cx.lineWidth + 2;
  }
  else{
    alert('Max limit reached');
  }
}

public lineDecrease(){
  if(this.cx.lineWidth > 2){
  this.cx.lineWidth = this.cx.lineWidth - 2;
  }
  else{
    alert('Min limit reached');
  }
}

public restetTemplate(){
  this.cx.clearRect(0, 0, this.canvasEl.width, this.canvasEl.height);
}

SetBackGroundTan(){
  this.canvasEl.style.backgroundColor = "Bisque";
}

SetBackGroundGreen(){
  this.canvasEl.style.backgroundColor = "Green";
}

SetBackGroundWhite(){
  this.canvasEl.style.backgroundColor = "White";

}

saveCanvas() {

  this.ImageString = this.canvasEl.toDataURL(); //1 indicates full quality
 // console.log(this.imageData);
  this.model.ImageString  = this.ImageString;
  console.log(this.model.ImageString);
  this.drawService.createDrawing(this.model).subscribe(response => {
    console.log(response);
  }), err => {
    console.log(err)
  }
}

getPlaybookId(){
//this.model.PlaybookId = this.userService.getTeam(); 
}
  
  //descrition
  //name
  //playbook Id
}



