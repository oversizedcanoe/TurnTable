import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScatComponent } from './scat/scat.component';
import { LinkFourComponent } from './link-four/link-four.component';
import { TicTacNoComponent } from './tic-tac-no/tic-tac-no.component';
import { WordTrainComponent } from './word-train/word-train.component';
import { Game5Component } from './game-5/game-5.component';
import { NgxCardsComponent } from 'ngx-cards';


@NgModule({
  declarations: [ScatComponent, LinkFourComponent, TicTacNoComponent, WordTrainComponent, Game5Component],
  imports: [
    CommonModule,
    NgxCardsComponent
  ],
  exports: [ScatComponent, LinkFourComponent, TicTacNoComponent, WordTrainComponent, Game5Component],
})
export class GamesModule { }
