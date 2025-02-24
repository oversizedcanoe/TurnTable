import { Component, OnInit } from '@angular/core';
import { GameService } from '../games.service';
import { GameType } from '../../../shared/models/enums';
import { GameDTO } from '../../../shared/models/models';
import Keyboard from 'simple-keyboard'
import 'simple-keyboard/build/css/index.css';

@Component({
  selector: 'app-word-golf',
  templateUrl: './word-golf.component.html',
  styleUrl: './word-golf.component.css'
})
export class WordGolfComponent implements OnInit {
  public readonly CHAR_COUNT: number = 8;
  public readonly WORD_COUNT: number = 6;
  public gameBoard: string[][];
  private allowedKeys: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
  private correctWordCount: number = 1;
  private words: string[] = [];
  public keyboard!: Keyboard;
  public isMobile: boolean = false;
  private readonly ENTER_KEY = 'enter'
  private readonly BACKSPACE_KEY = 'backspace'
  private focusedWordIndex: number = 1;
  private focusedCharIndex: number = 0;
  public guessedWords: string[] = [];
  public score: number = 0;
  public hintedLetters: string = '';
  public infoText: string = ''; 

  constructor(public gameService: GameService) {
    this.gameService.initialize(GameType.WordGolf);
    this.gameBoard = [];
    this.infoText = "Complete the course in the lowest score you can by guessing the next word until the course is complete.\n\n" +
      "Words can be part of a common phrase ('good', 'morning' = 'Good Morning') or part of a single word('bar', 'bell' = 'Barbell').\n\n" +
      "Incorrect guess: 1pt. Hint: 3pts. Par: 15.";
  }

  async ngOnInit(): Promise<void> {
    this.isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);

    this.focusInput(this.focusedWordIndex, this.focusedCharIndex);
    this.initializeKeyboard();

    for (let i = 0; i < this.WORD_COUNT; i++) {
      this.gameBoard[i] = Array<string>(this.CHAR_COUNT).fill('');
    }

    const gameCode = await this.gameService.newSinglePlayerGame();

    await this.updateGameState();
  }

  async updateGameState() {
    const gameState: GameDTO | null = await this.gameService.getGame();

    if (gameState == null) {
      alert('Failed to get game state, try refreshing');
      return;
    }

    this.words = gameState.gameState as string[];

    this.setWord(this.words[0], 0);
  }

  initializeKeyboard() {
    if (this.isMobile) {
      setTimeout(() => {
        this.keyboard = new Keyboard({
          onKeyPress: key => this.onMobileKeyPress(key),
          mergeDisplay: true,
          layoutName: "shift",
          layout: {
            shift: [
              "Q W E R T Y U I O P",
              "A S D F G H J K L",
              "{backspace} Z X C V B N M {backspace}",
              "{enter}"
            ],
          },
          display: {
            "{enter}": "Enter",
            "{backspace}": "⌫",
          }
        });
      }, 100);
    }

  }

  isCellDisabled(wordIndex: number, charIndex: number) {
    // Disable if this word index is not the correct wordIndex
    const rowIsDisabled = wordIndex != this.correctWordCount;
    let cellIsDisabled = false;

    if (rowIsDisabled == false) {
      // Possible that this cell is hinted, which should be disabled
      if (this.hintedLetters.length > charIndex) {
        cellIsDisabled = true;
      }
    }

    return rowIsDisabled || cellIsDisabled;
  }

  clearWord(wordIndex: number) {
    for (let i = 0; i < this.CHAR_COUNT; i++) {
      this.gameBoard[wordIndex][i] = '';
    }
  }

  setWord(word: string, wordIndex: number) {
    for (let i = 0; i < word.length; i++) {
      this.gameBoard[wordIndex][i] = word[i].toUpperCase();
    }
  }

  // Desktop: Letter is keydown, keyPress, oninput
  // Desktop: Enter is keydown, keyPress
  // Desktop: Backspace is keydown

  keyDown(wordIndex: number, charIndex: number, $event: Event) {
    $event.preventDefault();

    if ($event instanceof KeyboardEvent) {
      const key = $event.key;

      if (key == 'Backspace') {
        this.onBackspacePressed(wordIndex, charIndex);
      }
      else if (key == 'Enter') {
        this.onEnterPressed(wordIndex, charIndex);
      }
      else {
        this.onLetterKeyPressed(wordIndex, charIndex, key);
      }
    }
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
      this.focusInput(wordIndex, charIndex + 1);
    }
  }

  onBackspacePressed(wordIndex: number, charIndex: number) {
    if (this.gameBoard[wordIndex][charIndex] == '') {
      this.gameBoard[wordIndex][charIndex - 1] = '';

      if (charIndex > 0) {
        this.focusInput(wordIndex, charIndex - 1);
      }
    }
  }

  onFocus($event: FocusEvent) {
    const target = $event.target;

    if (target == null || target instanceof Element == false) {
      return;
    }

    this.focusedWordIndex = Number(target.getAttribute('data-wordIndex'));
    this.focusedCharIndex = Number(target.getAttribute('data-charIndex'));
  }

  onMobileKeyPress(key: string) {
    if (key.indexOf(this.BACKSPACE_KEY) > -1) {
      this.onBackspacePressed(this.focusedWordIndex, this.focusedCharIndex);
    }
    else if (key.indexOf(this.ENTER_KEY) > -1) {
      this.onEnterPressed(this.focusedWordIndex, this.focusedCharIndex);
    }
    else {
      this.onLetterKeyPressed(this.focusedWordIndex, this.focusedCharIndex, key);
    }
  }

  focusInput(wordIndex: number, charIndex: number) {
    setTimeout(() => {
      document.getElementById(`input${wordIndex}-${charIndex}`)?.focus();
    }, 50);
  }

  onEnterPressed(wordIndex: number, charIndex: number) {
    const currentWord = (this.gameBoard[wordIndex]).join('');

    if (this.words[wordIndex] == currentWord) {
      this.hintedLetters = '';
      this.handleWordGuessedCorrectly();
    }
    else {
      // word is wrong
      this.clearWord(wordIndex);
      this.setWord(this.hintedLetters, wordIndex);
      this.focusInput(wordIndex, this.hintedLetters.length);

      if (this.guessedWords.indexOf(currentWord) == -1) {
        // not yet guessed
        this.score += 1;
        this.guessedWords.push(currentWord);
      }
    }
  }

  handleWordGuessedCorrectly() {
    this.correctWordCount += 1;
    this.guessedWords = [];

    if (this.words.length == this.correctWordCount) {
      alert('Nice job!🎉');
    }
    else {
      this.focusInput(this.correctWordCount, 0);
    }
  }

  showHint() {
    this.score += 3;
    const wordIndexForHint: number = this.correctWordCount;
    this.clearWord(wordIndexForHint);
    const wordForHint: string = this.words[wordIndexForHint];
    const letterIndexForHint: number = this.hintedLetters.length;
    const letterForHint = wordForHint[letterIndexForHint];

    this.hintedLetters += letterForHint;
    this.setWord(this.hintedLetters, wordIndexForHint);

    if (this.hintedLetters == wordForHint) {
      // Word completed with hint, simulate enter key pressed
      this.onEnterPressed(wordIndexForHint, 0);
    }
    else {
      this.focusInput(wordIndexForHint, letterIndexForHint + 1);
    }
  }
}
