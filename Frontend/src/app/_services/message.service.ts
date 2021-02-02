import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getInboxes(id:string) {
    return this.http.get(this.baseUrl + `messages/inboxes/${id}`)
  }

  getMessages() {
    return this.http.get(this.baseUrl + 'messages')
  }

  getMessage(id:string) {
    return this.http.get(this.baseUrl + `messages/${id}`);
  }

  getSentMessages(id: string) {
    return this.http.get(this.baseUrl + `messages/sender/${id}`)
  }

  getRecipientLists() {
    return this.http.get(this.baseUrl + 'messages/recipientlists')
  }

  getRecipientList(id:string) {
    return this.http.get(this.baseUrl + `messages/recipientlists/${id}`);
  }

  sendMessage(message:any) {
    return this.http.post(this.baseUrl + `messages/send`, message);
  }

  sendCarpool(message:any) {
    return this.http.post(this.baseUrl + `messages/send/carpool`, message);
  }

}
