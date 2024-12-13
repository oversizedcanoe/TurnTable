import { Component, OnInit } from '@angular/core';
import { GameService } from '../games.service';

@Component({
  selector: 'app-word-train',
  templateUrl: './word-train.component.html',
  styleUrl: './word-train.component.css'
})
export class WordTrainComponent implements OnInit {
  public readonly CHAR_COUNT: number = 8;
  public readonly WORD_COUNT: number = 6;
  public gameBoard: string[][];
  private allowedKeys: string = 'abcdefghijklmnopqrstuvwxyz';
  private correctWordCount: number = 1;

  constructor(private gameService: GameService) {
    this.gameBoard = [];
  }
  ngOnInit(): void {
    for (let i = 0; i < this.WORD_COUNT; i++) {
      this.gameBoard[i] = Array<string>(this.CHAR_COUNT).fill('');
    }

    this.testSetWord('Test', 0);
  }

  onInput(wordIndex: number, charIndex: number, $event: Event) {
    $event.preventDefault();
    alert('something')

    if ($event instanceof InputEvent == false) {
      alert('Not an InputEvent?')
      return;
    }

    const key: string | null = $event.data;

    if (key == null || this.allowedKeys.indexOf(key) == -1) {
      return;
    }

    const upperCaseKey = key.toUpperCase();
    this.gameBoard[wordIndex][charIndex] = upperCaseKey;

    if (charIndex < this.CHAR_COUNT) {
      document.getElementById(`input${wordIndex}-${charIndex + 1}`)?.focus();
    }
    //else if (key == 'Enter') {
    //  alert('Enter pressed');
    //}
    //else if (key == 'Backspace') {
    //  if (this.gameBoard[wordIndex][charIndex] == '') {
    //    this.gameBoard[wordIndex][charIndex - 1] = '';

    //    if (charIndex > 0) {
    //      document.getElementById(`input${wordIndex}-${charIndex - 1}`)?.focus();
    //    }
    //  }
    //}
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
}
