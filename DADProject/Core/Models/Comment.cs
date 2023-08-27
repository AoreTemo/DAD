namespace Core.Models;

public class Comment : Content
{
    public int? PostId { get; set; }
    public Post? Post { get; set; }
    public int? RepliedCommentId { get; set; }
    public Comment? RepliedComment { get; set; }
    public List<Comment> Replies { get; set; } = new List<Comment>();
}
