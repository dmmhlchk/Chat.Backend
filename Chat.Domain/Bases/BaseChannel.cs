namespace Chat.Domain.Bases;

public abstract class BaseChannel
{
    public required string id { get; init; }
    public required string title { get; set; }
    
    // private settings
    public required bool isPrivate { get; set; } = false;
    public List<string>? participants { get; set; }
}