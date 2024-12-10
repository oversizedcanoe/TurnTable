import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { GameHubService } from './game-hub.service';
import { Subject } from 'rxjs';
import { NewGameDTO, JoinedGameDTO, GameDTO } from '../../shared/models/models';

@Injectable({
  providedIn: 'root'
})

export class GameService {
  private url: string = 'game';
  public onGameStateChanged: Subject<void> = new Subject();

  constructor(private backendService: BackendService, private gameHubService: GameHubService) { }

  async newGame(gameType: GameType, playerOneName: string): Promise<string> {
    var body = { gameType: gameType, playerOneName: playerOneName };

    let newGameCode: string = ''

    const result = await this.backendService.post<NewGameDTO>(this.url + '/new', body);

    if (result != null) {
      newGameCode = result.gameCode;
    }

    this.gameHubService.joinGroup(newGameCode);

    this.gameHubService.onGameStateChanged.subscribe(() => {
      console.warn("Game State Changed");
      this.onGameStateChanged.next();
    });

    return newGameCode;
  }

  async joinGame(gameCode: string, playerName: string): Promise<number> {
    this.gameHubService.joinGroup(gameCode);

    var body = { gameCode: gameCode, playerName: playerName };

    const result = await this.backendService.post<JoinedGameDTO>(this.url + '/join', body);

    if (result) {
      return result.playerNumber;
    }
    else {
      return -1;
    }
  }

  async getGame(gameCode: string): Promise<GameDTO | null> {
    const result = await this.backendService.get<GameDTO>(this.url + '/' + gameCode);

    if (result != null) {
      return result;
    }
    else {
      return null;
    }
  }

  async move(gameCode: string, playerNumber: number, arg1: any, arg2: any = 'NOT USED', arg3: any = 'NOT USED'): Promise<boolean> {
    var body = { gameCode: gameCode, playerNumber: playerNumber, arg1: arg1, arg2: arg2, arg3: arg3 };

    const result = await this.backendService.post<void>(this.url + '/move', body);

    return true;
  }
}
