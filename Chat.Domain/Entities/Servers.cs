using Chat.Domain.Bases;

namespace Chat.Domain.Entities;

public class Server : BaseServer
{
    public required Participant owner { get; init; }
    public required List<Role> roles { get; set; }
}

