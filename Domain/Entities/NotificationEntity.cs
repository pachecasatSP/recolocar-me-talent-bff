public class NotificationEntity : BaseEntity
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public DateTime? ViewedAt { get; set; }

    public Guid? Owner { get; set; }

    public NotificationStatus Status { get; set; } = NotificationStatus.New;
}