
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getCalendar() {
    return this.http.get(this.baseUrl + 'calendar');
  }

  getEvents() {
    return this.http.get(this.baseUrl + 'calendar/events');
  }

  createEvent(model: any) {
    return this.http.post(this.baseUrl + `calendar`, model);
  }

  deleteEvent(eventId:string) {
    return this.http.delete(this.baseUrl + `calendar/events/${eventId}`);
  }
}
