import { Component, OnInit } from '@angular/core';
import { GameService } from '../games.service';
import { GameType } from '../../../shared/models/enums';
import { GameDTO } from '../../../shared/models/models';

@Component({
  selector: 'app-word-train',
  templateUrl: './word-train.component.html',
  styleUrl: './word-train.component.css'
})
export class WordTrainComponent implements OnInit {
  public readonly CHAR_COUNT: number = 8;
  public readonly WORD_COUNT: number = 6;
  public gameBoard: string[][];
  private allowedKeys: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
  private correctWordCount: number = 1;
  private words: string[] = [];

  constructor(private gameService: GameService) {
    this.gameService.initialize(GameType.WordTrain);

    this.gameBoard = [];
  }

  async ngOnInit(): Promise<void> {
    for (let i = 0; i < this.WORD_COUNT; i++) {
      this.gameBoard[i] = Array<string>(this.CHAR_COUNT).fill('');
    }

    const gameCode = await this.gameService.newSinglePlayerGame();

    await this.updateGameState();
  }

  async updateGameState() {
    const gameState: GameDTO | null = await this.gameService.getGame();

    console.warn(gameState);

    if (gameState == null) {
      alert('Failed to get game state, try refreshing');
      return;
    }

    this.words = gameState.gameState as string[];

    this.testSetWord(this.words[0], 0);
  }

  isRowDisabled(wordIndex: number) {
    // only enable if this index is this.correctWordCount
    const isEnabled = wordIndex == this.correctWordCount
    return !isEnabled;
  }

  testSetWord(word: string, wordIndex: number) {
    for (let i = 0; i < word.length; i++) {
      this.gameBoard[wordIndex][i] = word[i].toUpperCase();
    }
  }

  // Desktop: Letter is keydown, keyPress, oninput
  // Desktop: Enter is keydown, keyPress
  // Desktop: Backspace is keydown

  // Mobile: Letter is keydown, oninput, keyup
  // Mobile: Enter is nothing
  // Mobile: Backspace is keydown, keyup

  // for letters only
  onInput(wordIndex: number, charIndex: number, $event: Event) {
    if ($event instanceof InputEvent && $event.data) {
      this.onLetterKeyPressed(wordIndex, charIndex, $event.data)
    }
  }

  // for backspace only
  keyDown(wordIndex: number, charIndex: number, $event: Event) {
    if ($event instanceof KeyboardEvent && $event.key == 'Backspace') {
      this.onBackspacePressed(wordIndex, charIndex);
    }
  }

  // for enter only
  focusOut(wordIndex: number, charIndex: number, $event: Event) {
    // NOTE:
    // I think this is the best way for Mobile and Desktop to capture Enter
    // However as long as there is an alert here this will loop forever (focus out occurs,
    // alert is shown, which loses focus, so alert is shown, which loses focus...)

    //alert('focusOut')
    console.warn($event);
  }

  onLetterKeyPressed(wordIndex: number, charIndex: number, key: string) {
    const upperCaseKey = key.toUpperCase();

    if (this.allowedKeys.indexOf(upperCaseKey) == -1) {
      console.warn('bad');

      const input = document.getElementById(`input${wordIndex}-${charIndex + 1}`);

      if (input) {
        input.textContent = ''
      }

      console.warn(this.gameBoard[wordIndex][charIndex]);
      return;
    }

    this.gameBoard[wordIndex][charIndex] = upperCaseKey;

    // Select next input
    if (charIndex < this.CHAR_COUNT) {
      document.getElementById(`input${wordIndex}-${charIndex + 1}`)?.focus();
    }
  }

  onEnterPressed(wordIndex: number, charIndex: number) {
    alert('Enter pressed');
  }

  onBackspacePressed(wordIndex: number, charIndex: number) {
    if (this.gameBoard[wordIndex][charIndex] == '') {
      this.gameBoard[wordIndex][charIndex - 1] = '';

      if (charIndex > 0) {
        document.getElementById(`input${wordIndex}-${charIndex - 1}`)?.focus();
      }
    }
  }
}
