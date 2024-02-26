using Chat.Domain.Bases;

namespace Chat.Domain.Entities;

public class TextChannel : BaseChannel
{
    public List<Message>? messages { get; set; }
    public List<string>? pinnedMessages { get; set; }
}

public class VoiceChannel : BaseChannel
{
    public List<string>? connections { get; set; }
}
