import { Component } from '@angular/core';

@Component({
  selector: 'app-link-four',
  templateUrl: './link-four.component.html',
  styleUrl: './link-four.component.css'
})
export class LinkFourComponent {
  public readonly ROW_COUNT: number = 6;
  public readonly COL_COUNT: number = 7;
  gameBoard: number[][];

  constructor() {
    this.gameBoard = [];

    for (let i = 0; i < this.ROW_COUNT; i++) {
      this.gameBoard[i] = Array<number>(this.COL_COUNT).fill(0);
    }
  }

  getNumberAtPosition(rowNumber: number, colNumber: number): number {
    return this.gameBoard[rowNumber][colNumber];
  }

  setCell(rowNumber: number, colNumber: number, playerNumber: 1 | 2): void {
    const element = document.querySelector(`#td${rowNumber}-${colNumber}`);

    if (element == null) {
      return;
    }

    if (playerNumber == 1) {
      element.classList.add('one');
    }
    else if (playerNumber == 2) {
      element.classList.add('two');
    }

    this.gameBoard[rowNumber][colNumber] = playerNumber;

    this.checkForWin();
  }

  cellClick(rowIndex: number, colIndex: number, playerNumber: 1 | 2): void {
    // get highest rowIndex (closest row to the bottom) that is 0 for the given colIndex
    let largestRowIndex = -1;

    for (let i = 0; i < this.ROW_COUNT; i++) {
      const colValue = this.getNumberAtPosition(i, colIndex);

      if (colValue == 0) {
        largestRowIndex = i;
      }
    }

    if (largestRowIndex == -1) {
      alert('column full');
      return;
    }
    else {
      this.setCell(largestRowIndex, colIndex, playerNumber);
    }
  }

  checkForWin(): void{
    //todo
  }
}