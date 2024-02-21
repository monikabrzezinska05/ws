export class BaseDto<T> {
  eventType: string;

  constructor(init?: Partial<T>) {
    this.eventType = this.constructor.name;
    Object.assign(this, init)
  }
}

export class ServerBroadcastClientDto extends BaseDto<ServerBroadcastClientDto> {
  broadcastValue? : string;
}

export class ServerEchosClientDto extends BaseDto<ServerEchosClientDto> {
  echoValue? : string;
}
