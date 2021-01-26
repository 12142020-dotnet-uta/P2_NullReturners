import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Sports Management';


  constructor(public auth: AuthService, private http: HttpClient) {}

  ngOnInit() {

  }

}
