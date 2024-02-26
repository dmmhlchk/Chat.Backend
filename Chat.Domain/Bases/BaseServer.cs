using Chat.Domain.Entities;

namespace Chat.Domain.Bases;

public abstract class BaseServer
{
    public required string id { get; init; }
    public required string title { get; set; }
    public byte[]? profileImage { get; set; }

    public List<Participant>? participants { get; set; }
    
    public List<string>? textChannels { get; set; }
    public List<string>? voiceChannels { get; set; }
}