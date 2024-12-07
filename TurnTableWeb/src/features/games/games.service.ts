import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';

@Injectable({
    providedIn: 'root'
})

export class GameService {
    private url: string = 'game';

    constructor(private backendService: BackendService) { }

    async newGame(gameType: GameType, playerOneName: string): Promise<string> {
        var body = { gameType: gameType, playerOneName: playerOneName };

        let newGameCode: string = ''

        const result = await this.backendService.post<NewGameDTO>(this.url + '/new', body);

        if (result != null) {
            newGameCode = result.gameCode;
        }

        return newGameCode;
    }
}

class NewGameDTO {
    public gameCode: string = '';
}