import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  // session: per tab
  // local: 'forever'
  constructor() { }

  get username(): string | null {
    return localStorage.getItem('username');
  }

  set username(username: string) {
    localStorage.setItem('username', username);
  }
}

