import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameDetailComponent } from './game-detail/game-detail.component';



@NgModule({
  declarations: [GameDetailComponent],
  imports: [
    CommonModule
  ],
  exports: [GameDetailComponent]
})
export class SharedModule { }
