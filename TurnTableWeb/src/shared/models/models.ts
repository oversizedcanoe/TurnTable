import { GameType } from "./enums";

export interface NewGameDTO {
  gameCode: string;
}

export interface JoinedGameDTO {
  playerNumber: number;
}

export interface PlayerDTO {
  playerNumber: number;
  playerName: string;
}

export interface GameDTO {
  gameCode: string;
  gameType: GameType;
  players: PlayerDTO[];
  gameState: object;
  currentPlayerTurn: number;
  playerWinner: number | null;
  gameOver: boolean;
}
