using System.Text.Json;
using Fleck;
using lib;

namespace ws;

public class ClientWantsToBroadcastServerDto : BaseDto
{
    public string messageContent { get; set; }
}

public class ClientWantsToBroadcastServer : BaseEventHandler<ClientWantsToBroadcastServerDto>
{
    public override Task Handle(ClientWantsToBroadcastServerDto dto, IWebSocketConnection socket)
    {
        var broadcast = new ServerBroadcastClient()
        {
            broadcastValue = "broadcast: " + dto.messageContent
        };
        var messageToClient = JsonSerializer.Serialize(broadcast);
        foreach (var client in ConnectionsList.clients)
        {
            if (client.Key != socket.ConnectionInfo.Id)
            {
                client.Value.Send(messageToClient);
            }
        }
        return Task.CompletedTask;
    }
}

public class ServerBroadcastClient : BaseDto
{
    public string broadcastValue { get; set; }
}