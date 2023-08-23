using Core.Enums;

namespace Core.Models;

public class Message : Content
{
    public MessageState MessageState { get; set; }
    public int? RepliedMessageId { get; set; } 
    public Message? RepliedMessage { get; set; } // Message that we replied
    public List<Message> Replies { get; set; } = new List<Message>(); // Messages that replies this message
    public int? RecipientId { get; set; }
    public AppUser? Recipient { get; set; }
}