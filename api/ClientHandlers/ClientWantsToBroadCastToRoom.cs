using System.Text.Json;
using Fleck;
using lib;
using ws.Commons;
using ws.Infrastructure;

namespace ws;

[ValidateDataAnnotations]
public class ClientWantsToBroadCastToRoomDto : BaseDto
{
    public string message { get; set; }
    public int roomId { get; set; }
}

public class ClientWantsToBroadCastToRoom : BaseEventHandler<ClientWantsToBroadCastToRoomDto>
{
    private readonly Repository _repository;

    public ClientWantsToBroadCastToRoom(Repository repository)
    {
        _repository = repository;
    }
    
    public override Task Handle(ClientWantsToBroadCastToRoomDto dto, IWebSocketConnection socket)
    {
        var message = new ServerBroadCastsMessageWithUsername()
        {
            message = dto.message,
            username = StateService.Connections[socket.ConnectionInfo.Id].UserName
        };
        _repository.CreateMessages(new DataModel()
        {
            username = StateService.Connections[socket.ConnectionInfo.Id].UserName,
            roomid = dto.roomId,
            message = dto.message
        });
        StateService.BroadCastToRoom(dto.roomId, JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }
}

public class ServerBroadCastsMessageWithUsername : BaseDto
{
    public string message { get; set; }
    public string username { get; set; }
}