import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { PlayersComponent } from './players/players.component';
import { TeamsComponent } from './teams/teams.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { RolesComponent } from './roles/roles.component';
import { DrawComponent } from './draw/draw.component';
import { PlayerdetailsComponent } from './players/playerdetails/playerdetails.component';
import { EditplayerComponent } from './players/editplayer/editplayer.component';
import { EquipmentRequestDetailsComponent } from './equipment/equipment-request-details/equipment-request-details.component';
import { EditEquipmentRequestComponent } from './equipment/edit-equipment-request/edit-equipment-request.component';
import { PlaysComponent } from './draw/plays/plays.component';
import { CreateEquipmentRequestComponent } from './equipment/create-equipment-request/create-equipment-request.component';
import { CreatePlayerComponent } from './players/create-player/create-player.component';
import { CalendarComponent } from './calendar/calendar.component';



const routes: Routes = [
  {path: 'players', component: PlayersComponent},
  {path: 'teams', component: TeamsComponent},
  {path: 'equipment', component: EquipmentComponent},
  {path: 'roles', component: RolesComponent},
  {path: 'plays', component: PlaysComponent},
  {path: 'draw', component: DrawComponent},
  {path: 'calendar', component: CalendarComponent},
  {path: 'players/details/:id', component: PlayerdetailsComponent},
  {path: 'players/edit/:id', component: EditplayerComponent},
  {path: 'players/create', component: CreatePlayerComponent},
  {path: 'equipment/details/:id', component: EquipmentRequestDetailsComponent},
  {path: 'equipment/edit/:id', component: EditEquipmentRequestComponent},
  {path: "equipment/create", component: CreateEquipmentRequestComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }