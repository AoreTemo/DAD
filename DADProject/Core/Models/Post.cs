namespace Core.Models;

public class Post : Content
{
    public string Subject { get; set; }
    public int LikesCount { get; set; } = 0;
    public List<Comment>? Comments { get; set; } = new List<Comment>();
}
