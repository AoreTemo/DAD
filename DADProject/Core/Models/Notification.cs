using Core.Enums;

namespace Core.Models;

public class Notification : BaseEntity
{
    public string Body { get; set; } = string.Empty;
    public DateTime SendTime { get; set; }
    public NotificationState NotificationState { get; set; }
    public string? AppUserId { get; set; } = string.Empty;
    public AppUser? AppUser { get; set; }
}