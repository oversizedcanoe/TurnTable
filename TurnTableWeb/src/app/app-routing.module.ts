import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ScatComponent } from '../features/games/scat/scat.component';
import { DashboardComponent } from '../features/dashboard/dashboard.component';
import { TicTacNoComponent } from '../features/games/tic-tac-no/tic-tac-no.component';
import { WordGolfComponent } from '../features/games/word-golf/word-golf.component';
import { Game5Component } from '../features/games/game-5/game-5.component';
import { LinkFourComponent } from '../features/games/link-four/link-four.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'scat', component: ScatComponent },
  { path: 'linkfour', component: LinkFourComponent },
  { path: 'tictacno', component: TicTacNoComponent },
  { path: 'wordgolf', component: WordGolfComponent },
  { path: 'game-5', component: Game5Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
