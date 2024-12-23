import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { GameHubService } from './game-hub.service';
import { Subject } from 'rxjs';
import { NewGameDTO, JoinedGameDTO, GameDTO } from '../../shared/models/models';
import { StorageService } from '../../shared/services/storage.service';

@Injectable({
  providedIn: 'root'
})

export class GameService {
  get playerName(): string | null {
    return this.storage.playerName;
  }

  set playerName(name: string) {
    this.storage.playerName = name;
  }

  get gameCode(): string {
    return this.storage.gameCode;
  }

  set gameCode(gameCode: string) {
    this.storage.gameCode = gameCode;
  }

  public playerNumber: number = 0;
  public gameType: GameType | undefined;
  public gameState: GameDTO | null = null;

  private url: string = 'game';
  public onGameStateChanged: Subject<void> = new Subject();

  constructor(private backendService: BackendService, private gameHubService: GameHubService, private storage: StorageService) { }

  initialize(gameType: GameType) {
    this.gameType = gameType;
  }

  isGameMultiplayer() {
    return this.gameType != GameType.WordTrain;
  }

  async newSinglePlayerGame() {
    var body = { gameType: this.gameType, playerOneName: 'singleplayer' };

    let newGameCode: string = ''

    const result = await this.backendService.post<NewGameDTO>(this.url + '/new', body);

    if (result != null) {
      newGameCode = result.gameCode;
      this.gameCode = newGameCode;
    }

    return newGameCode;
  }

  async newMultiplayerGame(playerOneName: string ): Promise<string> {
    var body = { gameType: this.gameType, playerOneName: playerOneName };

    let newGameCode: string = ''

    const result = await this.backendService.post<NewGameDTO>(this.url + '/new', body);

    if (result != null) {
      newGameCode = result.gameCode;
      this.playerName = playerOneName;
      this.playerNumber = 1;
      this.gameCode = newGameCode;
      await this.subscribeToHub(this.gameCode);
    }

    return newGameCode;
  }

  async joinGame(gameCode: string, playerName: string): Promise<number> {
    this.playerName = playerName;

    var body = { gameCode: gameCode, playerName: playerName };

    let playerNumber: number = -1;

    const result = await this.backendService.post<JoinedGameDTO>(this.url + '/join', body);

    if (result) {
      playerNumber = result.playerNumber;
      this.gameCode = gameCode;
      this.playerNumber = result.playerNumber;
      await this.subscribeToHub(gameCode);
    }

    return playerNumber;
  }

  async getGame(): Promise<GameDTO | null> {
    this.gameState = await this.backendService.get<GameDTO>(this.url + '/' + this.gameCode);
    return this.gameState;
  }

  async move(arg1: any, arg2: any = 'NOT USED', arg3: any = 'NOT USED'): Promise<boolean> {
    if (this.gameState?.currentPlayerTurn != this.playerNumber) {
      alert('Wait your turn!');
      return false;
    }

    if (this.gameState?.players.length == 1) {
      alert('You need two players to play this game.')
      return false;
    }

    if (this.gameState?.gameOver) {
      alert('Game is over!');
      return false;
    }

    var body = { gameCode: this.gameCode, playerNumber: this.playerNumber, arg1: arg1, arg2: arg2, arg3: arg3 };

    const result = await this.backendService.post<void>(this.url + '/move', body);

    return true;
  }

  async subscribeToHub(gameCode: string): Promise<void> {
    await this.gameHubService.joinGroup(gameCode);

    // Todo --> Maybe need to leave group/unsubscribe when game over/nav away
    this.gameHubService.onGameStateChanged.subscribe(() => {
      console.warn("Game State Changed");
      this.onGameStateChanged.next();
    });
  }

  async playAgain() {
    var body = { gameCode: this.gameCode, playerNumber: this.playerNumber };

    const result = await this.backendService.post<void>(this.url + '/playagain', body);

    return true;
  }
}
