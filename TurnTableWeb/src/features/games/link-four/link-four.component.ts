import { Component, OnInit } from '@angular/core';
import { GameService } from '../games.service';
import { GameType } from '../../../shared/models/enums';
import { GameDTO, PlayerDTO } from '../../../shared/models/models';
import { StorageService } from '../../../shared/services/storage.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-link-four',
  templateUrl: './link-four.component.html',
  styleUrl: './link-four.component.css'
})
export class LinkFourComponent implements OnInit {
  public readonly ROW_COUNT: number = 6;
  public readonly COL_COUNT: number = 7;
  public gameBoard: number[][];
  public showGame = false;
  public checkForWinSubject: Subject<void> = new Subject();

  constructor(public gameService: GameService) {
    this.gameService.initialize(GameType.LinkFour);

    this.gameBoard = [];
    gameService.onGameStateChanged.subscribe(async () => {
      await this.updateGameState();
    })
  }

  ngOnInit(): void {
    for (let i = 0; i < this.ROW_COUNT; i++) {
      this.gameBoard[i] = Array<number>(this.COL_COUNT).fill(0);
    }
  }

  async newGame() {
    this.showGame = true;
    await this.updateGameState();
  }

  async joinGame() {
    this.showGame = true;

    await this.updateGameState();
  }

  async cellClick(rowIndex: number, colIndex: number): Promise<void> {
    await this.gameService.move(colIndex);
  }

  async updateGameState(): Promise<void> {
    const gameState = await this.gameService.getGame();

    console.warn(gameState);

    if (gameState == null) {
      alert('Failed to update game state, try refreshing');
      return;
    }

    this.gameBoard = gameState.gameState as number[][];

    this.checkForWinSubject.next();
  }
}
