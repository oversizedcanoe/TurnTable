import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';

@Injectable({
    providedIn: 'root'
})

export class KeyService {
    url: string = 'game';

    constructor(public backendService: BackendService) { }

    async newGame(gameType: GameType, playerOneName: string): Promise<string> {
        var body = { gameType: gameType, playerOneName: playerOneName };

        const result = await this.backendService.post<string>(this.url + '/new', body);

        let newGameCode: string = ''

        if (result != null) {
            newGameCode = result;
        }

        return newGameCode;
    }
}
