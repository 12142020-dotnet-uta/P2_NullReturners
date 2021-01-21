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

@NgModule({
  declarations: [
    AppComponent,
    PlayersComponent,
    TeamsComponent,
    EquipmentComponent,
    RolesComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: 'players', component: PlayersComponent},
      {path: 'teams', component: TeamsComponent},
      {path: 'equipment', component: EquipmentComponent},
      {path: 'roles', component: RolesComponent},
    ]),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
