using re.colocar.me.talent.Domain.Interfaces.Repositories;

namespace re.colocar.me.talent.Infrastructure.Repositories;

public class UserRepository : BaseRepository<ProfileEntity>, IUserRepository
{
    private TalentDbContext _context;

    public UserRepository(TalentDbContext context)
    {
        _context = context;
    }

    public override void Delete(Guid Id)
    {
        throw new NotImplementedException();
    }

    public override ProfileEntity? Get(Guid Id)
    {
        throw new NotImplementedException();
    }

    public ProfileEntity? GetByUserName(string userName)
    {
        return _context.Users?.FirstOrDefault(x => x.UserName == userName);
    }

    public override IEnumerable<ProfileEntity>? ListAll()
    {
        throw new NotImplementedException();
    }

    public override ProfileEntity Save(ProfileEntity entity)
    {
        _context.Users?.Add(entity);
        _context.SaveChanges();

        return entity;  
    }
}