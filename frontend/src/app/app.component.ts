import { Component } from '@angular/core';
import {FormControl} from "@angular/forms";
import ReconnectingWebSocket from 'reconnecting-websocket';
import {BaseDto, ServerBroadcastClientDto, ServerEchosClientDto} from "../BaseDto";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'frontend';

  messages: string[] = [];

  ws: WebSocket = new WebSocket("ws://localhost:8181");
  messageContent = new FormControl('');

  constructor() {
    this.ws.onmessage = message => {
      const messageFromServer = JSON.parse(message.data) as BaseDto<any>
      // @ts-ignore
      this[messageFromServer.eventType].call(this, messageFromServer);
    }
  }

  ServerEchosClient(dto: ServerEchosClientDto){
    this.messages.push(dto.echoValue!);
  }

  ServerBroadcastClient(dto: ServerBroadcastClientDto) {
    this.messages.push(dto.broadcastValue!);
  }

  sendMessageEcho() {
    var object = {
      eventType: "ClientWantsToEchoServer",
      messageContent: this.messageContent.value!
    }
    this.ws.send(JSON.stringify(object));
  }

  sendMessageBroadcast() {
    var object = {
      eventType: "ClientWantsToBroadcastServer",
      messageContent: this.messageContent.value!
    }
    this.ws.send(JSON.stringify(object));
  }
}
