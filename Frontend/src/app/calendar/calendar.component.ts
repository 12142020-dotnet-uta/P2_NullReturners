import { Component, OnInit, ViewChild } from '@angular/core';
import { Calendar } from '@fullcalendar/core';
import googleCalendarPlugin from '@fullcalendar/google-calendar';
import { AccountService } from '../_services/account.service';
import { CalendarService } from '../_services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  constructor(private calendarService: CalendarService) { }
  @ViewChild('calendar') model: any;//calendarService;
calendar: any

  ngOnInit(): void {
    this.getCalendar();
  }

  getCalendar() {
    this.calendarService.getCalendar().subscribe(response => { 
      this.model = response;
      console.log(this.model)
    }), err => {
      console.log(err);
    }    
  }

}
