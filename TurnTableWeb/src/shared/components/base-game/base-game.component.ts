import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameService } from '../../../features/games/games.service';
import { GameType } from '../../models/enums';
import { StorageService } from '../../services/storage.service';
import { PlayerDTO } from '../../models/models';

@Component({
  selector: 'app-base-game',
  templateUrl: './base-game.component.html',
  styleUrl: './base-game.component.css'
})
export class BaseGameComponent implements OnInit {
  public gameCodeToJoin: string = ''
  public playerName: string = '';
  public title: string = '';
  public showLogin: boolean = false;
  public showGameDetails: boolean = false;

  @Input() gameService!: GameService;
  @Output() onNewGame = new EventEmitter<void>();
  @Output() onJoinGame = new EventEmitter<void>();

  constructor(private storage: StorageService) {
  }

  ngOnInit(): void {
    this.playerName = this.gameService.playerName ?? '';
    this.showLogin = this.gameService.isGameMultiplayer();
    this.title = GameType[this.gameService.gameType ?? -1];
  }

  async onStartClicked() {
    const gameCode = await this.gameService.newMultiplayerGame(this.playerName);
    this.showLogin = false;

    if (this.gameService.isGameMultiplayer()) {
      this.showGameDetails = true;
    }

    this.onNewGame.next();
  }

  async onJoinClicked() {
    const playerNumber = await this.gameService.joinGame(this.gameCodeToJoin.toUpperCase(), this.playerName)

    if (playerNumber == -1) {
      alert('Unable to join game');
      return;
    }

    this.showLogin = false;

    if (this.gameService.isGameMultiplayer()) {
      this.showGameDetails = true;
    }

    this.onJoinGame.next();
  }

  isButtonDisabled_Start() {
    return this.playerName == '';
  }

  isButtonDisabled_Join() {
    return this.playerName == '' || this.gameCodeToJoin == '';
  }

  getClass(playerNumber: number): string {
    let classString = "";

    if (this.gameService.gameState?.currentPlayerTurn == playerNumber) {
      classString += " currentTurn ";
    }

    if (this.gameService.playerNumber == playerNumber) {
      classString += " bold ";
    }

    return classString;
  }

  getPlayers(): PlayerDTO[] {
    return this.gameService.gameState?.players ?? [];
  }

  getGameCode(): string {
    return this.gameService.gameState?.gameCode ?? '';
  }
}
