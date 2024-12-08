import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
    providedIn: 'root'
})

export class HubClientService {
  constructor(private backendService: BackendService) { }

  test() {
    const connection = new HubConnectionBuilder()
      .withUrl("/gamehub")
      .build();
  }
}

