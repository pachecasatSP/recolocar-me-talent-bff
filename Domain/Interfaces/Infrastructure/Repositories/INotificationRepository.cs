namespace re.colocar.me.talent.Domain.Interfaces.Repositories;

public interface INotificationRepository{
    int GetCountByOwner(Guid Owner);
    IEnumerable<NotificationEntity>? ListAll();
}