import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMessages() {
    return this.http.get(this.baseUrl + 'messages')
  }

  getRecipientLists() {
    return this.http.get(this.baseUrl + 'messages/recipientlists')
  }

  getRecipientList(id:string) {
    return this.http.get(this.baseUrl + `messages/recipientlists/${id}`);
  }

}
