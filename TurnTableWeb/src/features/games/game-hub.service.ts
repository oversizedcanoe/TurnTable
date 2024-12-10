import { Injectable } from '@angular/core';
import { BackendService } from '../../shared/services/backend.service';
import { GameType } from '../../shared/models/enums';
import { HubConnectionBuilder, HubConnection, HubConnectionState } from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class GameHubService {
  public static readonly HubUrl: string = "/gamehub"

  private hubConnection: HubConnection;
  public onGameStateChanged: Subject<void> = new Subject();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(BackendService.BackendUrl + GameHubService.HubUrl)
      .build();

    this.hubConnection.onclose(async () => {
      await this.connectAsync();
    });

    this.hubConnection.on("GameStateChanged", async () => {
      this.onGameStateChanged.next();
    })
  }

  async connectAsync() {
    console.warn(this.hubConnection.state);
    if (this.hubConnection.state != HubConnectionState.Connected) {
      await this.hubConnection.start();
    }
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
