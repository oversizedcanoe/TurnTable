import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { JoinGameData } from '../../shared/models/models';

@Injectable({
    providedIn: 'root'
})

export class GameHubService {
  public static readonly HubUrl: string = "/gamehub"

  private hubConnection: HubConnection;
  public onJoin: Subject<JoinGameData> = new Subject();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(BackendService.BackendUrl + GameHubService.HubUrl)
      .build();

    this.hubConnection.on("JoinGame", async (playerName: string, playerNumber: number) => {
      this.onJoin.next({ playerName: playerName, playerNumber: playerNumber });
    })
  }

  async connectAsync() {
    await this.hubConnection.start();
  }

  async sendEventToServerAsync() {
    await this.connectAsync();
    const someParameter = 5;
    await this.hubConnection.invoke("ServerMethodName", someParameter);
  }

  async joinGroup(groupName: string) {
    await this.connectAsync()
    this.hubConnection.invoke('JoinGroup', groupName);
  }
}
