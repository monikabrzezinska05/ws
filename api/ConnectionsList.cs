using Fleck;

namespace ws;

public static class ConnectionsList
{
    public static Dictionary<Guid, IWebSocketConnection> clients = new Dictionary<Guid, IWebSocketConnection>();
}