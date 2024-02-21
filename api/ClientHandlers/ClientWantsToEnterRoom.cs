using System.Text.Json;
using Fleck;
using lib;
using ws.Commons;
using ws.Infrastructure;

namespace ws;
[ValidateDataAnnotations]
public class ClientWantsToEnterRoomDto : BaseDto
{
    public int roomId { get; set; }
}

public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
{
    private readonly Repository _repository;

    public ClientWantsToEnterRoom(Repository repository)
    {
        _repository = repository;
    }
    
    public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        var isSucces = StateService.AddToRoom(socket, dto.roomId);
        
        socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
        {
            message = "You were succesfully added to room with ID" + dto.roomId,
            MessagesList = _repository.GetMessages(dto.roomId).ToList()
        }));
    return Task.CompletedTask;
    }
}

public class ServerAddsClientToRoom : BaseDto        
{                                                       
    public string message { get; set; }
    public List<DataModel>? MessagesList { get; set; }
}                                                       