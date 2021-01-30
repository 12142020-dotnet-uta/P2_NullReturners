
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  //getCalendarInitialized(){
  //return this.http.get(this.baseUrl + 'calendar');
  //}

  getCalendar() {
    return this.http.get(this.baseUrl + 'calendar/');
  }

  getMyEvents() {
    return this.http.get(this.baseUrl + 'calendar/events/');
  }

  createMyEvent(model: any) {
    return this.http.post(this.baseUrl + `calendar/`, model);
  }
}
