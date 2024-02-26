namespace Chat.Domain.Bases;

public abstract class BaseUser
{
    public required string id { get; init; }
    public required string username { get; set; }
    public byte[]? profileImage { get; set; }
}