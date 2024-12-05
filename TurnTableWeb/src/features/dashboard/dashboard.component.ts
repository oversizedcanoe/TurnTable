import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  games: any[] = [];

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.games = [
      { title: 'Scat 💥', url: '/scat', playerMin: 2, playerMax: 4, online: true, description: "Get the closest to 31." },
      { title: 'Link Four 🟢', url: '/linkfour', playerMin: 1, playerMax: 2, online: true, description: "Be the first to link four tokens in a row." },
      { title: 'Tic Tac No ❌', url: '/tictacno', playerMin: 1, playerMax: 2, online: true, description: "Be the LAST to link four tokens in a row." },
      { title: 'Word Train 🚂', url: '/wordtrain', playerMin: 1, playerMax: 1, online: false, description: "Guess all the words in as few guesses as possible." },
      // { title: 'Game 5', url: '/game-5', playerMin: 1, playerMax: 8, online: true, description: "Ut a mi porta, viverra lorem ornare, viverra ante." }
    ];
  }

  gameClicked(title: string) {
    this.router.navigateByUrl(this.games.filter(g => g.title == title)[0].url);
  }

}
