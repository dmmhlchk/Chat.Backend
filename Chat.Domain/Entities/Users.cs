using Chat.Domain.Bases;

namespace Chat.Domain.Entities;

public class User : BaseUser
{
    public List<string>? contacts { get; set; }
    public List<string>? servers { get; set; }
    
    public DateTime lastSeenRecently { get; set; }
}


public class Participant : BaseUser
{
    public required List<string> roles { get; set; } = new();
    public List<string>? pinnedMessages { get; set; }
}