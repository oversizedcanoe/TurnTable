import { Component, Input } from '@angular/core';
import { GameDTO } from '../../models/models';
import { GameType } from '../../models/enums'

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrl: './game-detail.component.css'
})
export class GameDetailComponent {

  @Input() playerNumber!: number;
  @Input() gameState!: GameDTO;

  public GameType = GameType

  constructor() {

  }

  getClass(playerNumber: number): string {
    let classString = "";

    if (this.gameState.currentPlayerTurn == playerNumber) {
      classString += " currentTurn ";
    }

    if (this.playerNumber == playerNumber) {
      classString += " bold ";
    }

    return classString;
  }

}
