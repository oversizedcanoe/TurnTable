import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseGameComponent } from './base-game/base-game.component';

@NgModule({
  declarations: [BaseGameComponent],
  imports: [
    CommonModule,
    FormsModule,
  ],
  exports: [BaseGameComponent]
})
export class SharedModule { }
