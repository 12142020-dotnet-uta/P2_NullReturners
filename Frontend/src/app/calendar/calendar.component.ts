import { Component, OnInit, ViewChild } from '@angular/core';

import { AccountService } from '../_services/account.service';
import { CalendarService } from '../_services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  constructor(private calendarService: CalendarService, public accountService: AccountService) { }
  @ViewChild('calendar') model: any;//calendarService;
calendar: any

events:any = [];

  ngOnInit(): void {
    this.getCalendar();
    this.getEvents();
  }

  getCalendar() {
    this.calendarService.getCalendar().subscribe(response => { 
      this.model = response;
      console.log(this.model)
    }, err => {
      console.log(err);
    })  
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
