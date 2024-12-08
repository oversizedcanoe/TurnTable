import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { GameHubService } from './game-hub.service';

@Injectable({
  providedIn: 'root'
})

export class GameService {
  private url: string = 'game';

  constructor(private backendService: BackendService, private gameHubService: GameHubService) { }

  async newGame(gameType: GameType, playerOneName: string): Promise<string> {
    var body = { gameType: gameType, playerOneName: playerOneName };

    let newGameCode: string = ''

    const result = await this.backendService.post<NewGameDTO>(this.url + '/new', body);

    if (result != null) {
      newGameCode = result.gameCode;
    }

    this.gameHubService.joinGroup(newGameCode);

    this.gameHubService.onJoin.subscribe((a) => {
      console.warn(a);
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
}

interface NewGameDTO {
  gameCode: string;
}

interface JoinedGameDTO {
  playerNumber: number;
}
