using re.colocar.me.talent.Domain.Interfaces.Repositories;

namespace re.colocar.me.talent.Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<NotificationEntity>, INotificationRepository
{
    private TalentDbContext _context;
    public NotificationRepository(TalentDbContext dbContext)
    {
        _context = dbContext;
    }

    public override NotificationEntity? Get(Guid Id)
    {
        return _context.Notifications?.Where(x => x.Id == Id).FirstOrDefault();
    }

    public override IEnumerable<NotificationEntity>? ListAll()
    {
        return _context.Notifications?.ToList();
    }

    public int GetCountByOwner(Guid owner)
    {
        return _context.Notifications?.Where(x => x.Owner == owner && x.Status == NotificationStatus.New).Count() ?? 0;
    }

    public override NotificationEntity Save(NotificationEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            _context.Notifications?.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        return null;
    }

    public override void Delete(Guid Id)
    {
        var entity = _context.Notifications?.FirstOrDefault(x => x.Id == Id);
        if (entity != null)
        {
            entity.ModifiedAt = DateTime.UtcNow;
            entity.Status = NotificationStatus.Deleted;
            _context.SaveChanges();
        }
    }

}