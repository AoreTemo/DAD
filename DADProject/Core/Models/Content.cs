namespace Core.Models;

public abstract class Content : BaseEntity
{
    public string Body { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public AppUser Author { get; set; } 
    public DateTime WriteTime { get; set; }
    public List<Media>? Media { get; set; }
}