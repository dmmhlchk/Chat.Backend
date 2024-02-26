using Chat.Domain.Enums;

namespace Chat.Domain.Entities;

public class Message : BaseEntity
{
    public required string sender { get; init; }
    public required DataType dataType { get; init; }
    public required DateTime dateTime { get; init; } = DateTime.Now;
    
    public string? textContent { get; set; }
}

