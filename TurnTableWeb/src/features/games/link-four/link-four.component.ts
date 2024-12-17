import { Component, OnInit } from '@angular/core';
import { GameService } from '../games.service';
import { GameType } from '../../../shared/models/enums';
import { GameDTO, PlayerDTO } from '../../../shared/models/models';
import { StorageService } from '../../../shared/services/storage.service';

@Component({
  selector: 'app-link-four',
  templateUrl: './link-four.component.html',
  styleUrl: './link-four.component.css'
})
export class LinkFourComponent implements OnInit {
  public gameCode: string = '';
  public gameCodeToJoin: string = ''
  public playerName: string = '';
  public playerNumber: number = 0;
  public readonly ROW_COUNT: number = 6;
  public readonly COL_COUNT: number = 7;
  public currentPlayerTurn: number = 0;
  public players: PlayerDTO[] = [];
  public gameBoard: number[][];
  public gameState!: GameDTO;

  constructor(private gameService: GameService, private storage: StorageService) {
    this.gameBoard = [];
    gameService.onGameStateChanged.subscribe(async () => {
      await this.updateGameState();
    })
  }

  ngOnInit(): void {
    for (let i = 0; i < this.ROW_COUNT; i++) {
      this.gameBoard[i] = Array<number>(this.COL_COUNT).fill(0);
    }

    this.playerName = this.storage.username ?? '';
  }

  async onStartClicked() {
    this.storage.username = this.playerName;
    const gameCode = await this.gameService.newGame(GameType.LinkFour, this.playerName);
    this.gameCode = gameCode;
    this.playerNumber = 1;
    await this.updateGameState();
  }

  isButtonDisabled_Start() {
    return this.playerName == '';
  }

  async onJoinClicked() {
    this.storage.username = this.playerName;

    this.gameCodeToJoin = this.gameCodeToJoin.toUpperCase();

    const playerNumber = await this.gameService.joinGame(this.gameCodeToJoin, this.playerName)

    if (playerNumber == -1) {
      alert('Unable to join game');
      return;
    }

    this.playerNumber = playerNumber;
    this.gameCode = this.gameCodeToJoin;
    await this.updateGameState();
  }

  isButtonDisabled_Join() {
    return this.playerName == '' || this.gameCodeToJoin == '';
  }

  async cellClick(rowIndex: number, colIndex: number): Promise<void> {
    if (this.isMyTurn() == false) {
      alert('Wait your turn!');
      return;
    }
    if (this.players.length == 1) {
      alert('You need two players to play this game.')
      return;
    }

    await this.gameService.move(this.gameCode, this.playerNumber, colIndex);
  }

  async updateGameState(): Promise<void> {
    const gameState = await this.gameService.getGame(this.gameCode);

    console.warn(gameState);

    if (gameState == null) {
      alert('Failed to update game state, try refreshing');
      return;
    }

    this.gameState = gameState;

    this.players = gameState.players.sort((a: PlayerDTO, b: PlayerDTO) => a.playerNumber - b.playerNumber);
    this.gameBoard = gameState.gameState as number[][];

    // Short delay to ensure game board updates before showing prompt
    setTimeout(() => {
      if (gameState.playerWinner) {
        if (gameState.playerWinner == this.playerNumber) {
          alert(`You won 😃`)
        } else {
          alert(`${this.players.find(p => p.playerNumber == gameState.playerWinner)?.playerName} won 😞`)
        }
      }
      else if (gameState.gameOver) {
        alert('Game over');
      }
      else {
        this.currentPlayerTurn = gameState.currentPlayerTurn;
      }
    }, 150);

  }

  isMyTurn(): boolean {
    return this.currentPlayerTurn == this.playerNumber;
  }
}
