import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameService } from '../../../features/games/games.service';
import { GameType } from '../../models/enums';
import { StorageService } from '../../services/storage.service';
import { PlayerDTO } from '../../models/models';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

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
  public gameCodeLink: string = '';
  public gameCodeButtonText: string = '';

  @Input() gameService!: GameService;
  @Input() checkForWin: Subject<void> = new Subject();
  @Output() onNewGame = new EventEmitter<void>();
  @Output() onJoinGame = new EventEmitter<void>();

  constructor(private storage: StorageService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.playerName = this.gameService.playerName ?? '';
    this.showLogin = this.gameService.isGameMultiplayer();
    this.title = GameType[this.gameService.gameType ?? -1];

    const gameCodeQueryParam: string | null = this.route.snapshot.queryParamMap.get('gameCode');

    if (gameCodeQueryParam) {
      this.gameCodeToJoin = gameCodeQueryParam;
    }

    const params = { ...this.route.snapshot.queryParams };
    delete params['gameCode'];
    this.router.navigate([], { queryParams: params })

    this.checkForWin.subscribe(() => {
      // Short delay to ensure game board updates before showing prompt
      setTimeout(() => {
        this.onCheckForWin();
      }, 150);
    })
  }

  async onStartClicked() {
    const gameCode = await this.gameService.newMultiplayerGame(this.playerName);
    this.showLogin = false;

    if (this.gameService.isGameMultiplayer()) {
      this.showGameDetailsSection();
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
      this.showGameDetailsSection();
    }

    this.onJoinGame.next();
  }

  onCheckForWin() {
    const gameState = this.gameService.gameState;

    if (gameState == null) {
      return;
    }

    if (gameState.playerWinner) {
      if (gameState.playerWinner == this.gameService.playerNumber) {
        alert(`You won 😃`)
      } else {
        alert(`${gameState.players.find(p => p.playerNumber == gameState.playerWinner)?.playerName} won 😞`)
      }
    }
    else if (gameState.gameOver) {
      alert('Game over');
    }

    if (confirm("Play again?")) {
      if (this.getPlayerNumber() == 1) {
        //start game
      }

    }
  }

  showGameDetailsSection(): void {
    this.showGameDetails = true;
    this.gameCodeLink = window.location.origin + window.location.pathname + "?gameCode=" + this.gameService.gameCode;
    this.gameCodeButtonText = this.gameService.gameCode + ' ✂';
  }

  isButtonDisabled_Start() {
    return this.playerName == '' || this.gameCodeToJoin != '';
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

  getPlayerNumber(): number {
    return this.getPlayers().filter(p => p.playerName == this.playerName)[0].playerNumber;
  }

  async copyLink() {
    if (navigator.clipboard && window.isSecureContext) {
      await navigator.clipboard.writeText(this.gameCodeLink);
    }
    else {
      // Use the 'out of viewport hidden text area' trick
      const textArea = document.createElement("textarea");
      textArea.value = this.gameCodeLink;

      // Move textarea out of the viewport so it's not visible
      textArea.style.position = "absolute";
      textArea.style.left = "-999999px";

      document.body.prepend(textArea);
      textArea.select();

      try {
        document.execCommand('copy');
      } catch (error) {
        console.error(error);
      } finally {
        textArea.remove();
      }
    }

    this.gameCodeButtonText = this.gameService.gameCode + ' 👍';

    setTimeout(() => {
      this.gameCodeButtonText = this.gameService.gameCode + ' ✂';
    }, 3000);

  }
}
