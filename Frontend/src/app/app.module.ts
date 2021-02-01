import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PlayersComponent } from './players/players.component';
import { TeamsComponent } from './teams/teams.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { RolesComponent } from './roles/roles.component';
import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './nav/nav.component';
import { DrawComponent } from './draw/draw.component';
import { PlayerdetailsComponent } from './players/playerdetails/playerdetails.component';
import { EditplayerComponent } from './players/editplayer/editplayer.component';
import { EquipmentRequestDetailsComponent } from './equipment/equipment-request-details/equipment-request-details.component';
import { EditEquipmentRequestComponent } from './equipment/edit-equipment-request/edit-equipment-request.component';
import { PlaysComponent } from './draw/plays/plays.component';
import { CreateEquipmentRequestComponent } from './equipment/create-equipment-request/create-equipment-request.component';
import { CreatePlayerComponent } from './players/create-player/create-player.component';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { CalendarComponent } from './calendar/calendar.component';
import { GamesComponent } from './games/games.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { EditGameComponent } from './games/edit-game/edit-game.component';
import { CreateEventComponent } from './calendar/create-event/create-event.component';
import { DeleteEventsComponent } from './calendar/delete-events/delete-events.component';


@NgModule({
  declarations: [
    AppComponent,
    PlayersComponent,
    TeamsComponent,
    EquipmentComponent,
    RolesComponent,
    NavComponent,
    DrawComponent,
    PlayerdetailsComponent,
    EditplayerComponent,
    EquipmentRequestDetailsComponent,
    EditEquipmentRequestComponent,
    PlaysComponent,
    CreateEquipmentRequestComponent,
    CreatePlayerComponent,
    CalendarComponent,
    HomeComponent,
    MessagesComponent,
    GamesComponent,
    CreateGameComponent,
    EditGameComponent,
    CreateEventComponent,
    DeleteEventsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
