import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  // session: per tab
  // local: 'forever'
  constructor() { }

  get playerName(): string {
    return localStorage.getItem('playerName') ?? '';
  }

  set playerName(playerName: string) {
    localStorage.setItem('playerName', playerName);
  }

  get gameCode(): string {
    return localStorage.getItem('gameCode') ?? '';
  }

  set gameCode(gameCode: string) {
    localStorage.setItem('gameCode', gameCode);
  }
}

