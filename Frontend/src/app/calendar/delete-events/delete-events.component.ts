import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { CalendarService } from 'src/app/_services/calendar.service';

@Component({
  selector: 'app-delete-events',
  templateUrl: './delete-events.component.html',
  styleUrls: ['./delete-events.component.css']
})
export class DeleteEventsComponent implements OnInit {

  constructor(public accountService: AccountService, private calendarService: CalendarService) { }

  events:any = [];
  
  ngOnInit(): void {
    this.getEvents();
  }

  getEvents() {
    this.calendarService.getEvents().subscribe(events => {
      this.events = events;
      console.log(events);
    }, err => {
      console.log(err);
    })
  }

  deleteEvent(eventId:string) {
    this.calendarService.deleteEvent(eventId).subscribe(event => {
      console.log(event);
      this.getEvents();
    }, err => {
      console.log(err);
    })
  }

}
