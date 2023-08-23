namespace Core.Models;

public class Post : Content
{
    public string Subject { get; set; } = string.Empty;
    public int LikesCount { get; set; } = 0;
    public List<Comment>? Comments { get; set; }
}
