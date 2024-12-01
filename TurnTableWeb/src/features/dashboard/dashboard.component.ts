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
      { title: 'Game 1', url: '/game-1', playerMin: 1, playerMax: 4, online: true, description: "Curabitur semper nisi et tortor viverra ullamcorper." },
      { title: 'Link Four', url: '/link-four', playerMin: 1, playerMax: 1, online: true, description: "Vestibulum quis dui sapien." },
      { title: 'Game 3', url: '/game-3', playerMin: 1, playerMax: 2, online: false, description: "Mauris sit amet ex id justo finibus molestie." },
      { title: 'Game 4', url: '/game-4', playerMin: 1, playerMax: 1, online: false, description: "Sed malesuada libero diam, porttitor venenatis eros interdum et. In porta magna fringilla nisi placerat feugiat." },
      { title: 'Game 5', url: '/game-5', playerMin: 1, playerMax: 8, online: true, description: "Ut a mi porta, viverra lorem ornare, viverra ante." }
    ];
  }

  gameClicked(title: string) {
    this.router.navigateByUrl(this.games.filter(g => g.title == title)[0].url);
  }

}
