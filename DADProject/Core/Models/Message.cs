using Core.Enums;

namespace Core.Models;

public class Message : Content
{
    public MessageState MessageState { get; set; }
    
    public int? RepliedMessageId { get; set; }
    public Message? RepliedMessage { get; set; }
    
    public List<Message>? Replies { get; set; }

    public int RecipientId { get; set; }
    public AppUser Recipient { get; set; }
}