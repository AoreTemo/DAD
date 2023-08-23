namespace Core.Models;

public class Comment : Content
{
    public int PostId { get; set; }
    public Post Post { get; set; }
    public List<Comment> Replies { get; set; } = new List<Comment>(); // Replies on this comment
    public int? RepliedCommentId { get; set; }
    public Comment? RepliedComment { get; set; } // Comment that we are replying
}