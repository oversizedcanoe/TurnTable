import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Game1Component } from '../features/games/game-1/game-1.component';
import { DashboardComponent } from '../features/dashboard/dashboard.component';
import { Game2Component } from '../features/games/game-2/game-2.component';
import { Game3Component } from '../features/games/game-3/game-3.component';
import { Game4Component } from '../features/games/game-4/game-4.component';
import { Game5Component } from '../features/games/game-5/game-5.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'game-1', component: Game1Component },
  { path: 'game-2', component: Game2Component },
  { path: 'game-3', component: Game3Component },
  { path: 'game-4', component: Game4Component },
  { path: 'game-5', component: Game5Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
