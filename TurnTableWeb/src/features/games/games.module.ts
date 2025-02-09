import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScatComponent } from './scat/scat.component';
import { LinkFourComponent } from './link-four/link-four.component';
import { TicTacNoComponent } from './tic-tac-no/tic-tac-no.component';
import { WordGolfComponent } from './word-golf/word-golf.component';
import { Game5Component } from './game-5/game-5.component';
//import { NgxCardsComponent } from 'ngx-cards';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/components/shared.module';

@NgModule({
  declarations: [ScatComponent, LinkFourComponent, TicTacNoComponent, WordGolfComponent, Game5Component],
  imports: [
    CommonModule,
   // NgxCardsComponent,
    SharedModule  
  ],
  exports: [ScatComponent, LinkFourComponent, TicTacNoComponent, WordGolfComponent, Game5Component],
})
export class GamesModule { }
