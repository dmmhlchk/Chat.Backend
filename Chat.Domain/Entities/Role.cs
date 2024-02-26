namespace Chat.Domain.Entities;

public class Role : BaseEntity
{
    public required string name { get; set; }
    public required string color { get; set; }
    
    // general permissions
    public required bool viewChannel { get; set; } = true;
    public required bool manageChannels { get; set; } = false;
    
    public required bool createInvite { get; set; } = true;
    
    public required bool changeUsername { get; set; } = true;
    public required bool manageUsernames { get; set; } = false;
    
    
    // text channel permissions
    public required bool readHistory { get; set; } = false;
    public required bool sendMessage { get; set; } = true;
    public required bool manageMessages { get; set; } = false;
    
    
    // video channel permissions
    public required bool connect { get; set; } = true;
    public required bool speak { get; set; } = true;
    public required bool video { get; set; } = true;
}