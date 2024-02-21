using System.Text.Json;
using Fleck;
using Serilog;
using ws.Commons;
using ws.Infrastructure.Modeller;

namespace ws;

public static class GlobalExceptionHandler
{
    public static void Handle(
        this Exception exception,
        IWebSocketConnection socketConnection,
        string? message)
    {
        Log.Error(exception, "Global Exception Handler");
        socketConnection.Send(JsonSerializer.Serialize(new ServerSendsErrorMessageToClient()
            {
                receivedMessage = message,
                errorMessage = exception.Message
            },
            new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }));
        if (exception is JwtVerificationException)
        {
            // Do this some other time
            //socketConnection.UnAuthenticate();
            //socketConnection.Send(new ServerRejectsClientJwt());
        }

    }
}