using lib;

namespace ws.Infrastructure.Modeller;

public class ServerSendsErrorMessageToClient : BaseDto
{
    public string? errorMessage { get; set; }
    public string? receivedMessage { get; set; }
}