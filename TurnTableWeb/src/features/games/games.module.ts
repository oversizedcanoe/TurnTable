import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Game1Component } from './game-1/game-1.component';
import { LinkFourComponent } from './link-four/link-four.component';
import { Game3Component } from './game-3/game-3.component';
import { Game4Component } from './game-4/game-4.component';
import { Game5Component } from './game-5/game-5.component';
import { NgxCardsComponent } from 'ngx-cards';


@NgModule({
  declarations: [Game1Component, LinkFourComponent, Game3Component, Game4Component, Game5Component],
  imports: [
    CommonModule,
    NgxCardsComponent
  ],
  exports: [Game1Component, LinkFourComponent, Game3Component, Game4Component, Game5Component],
})
export class GamesModule { }
