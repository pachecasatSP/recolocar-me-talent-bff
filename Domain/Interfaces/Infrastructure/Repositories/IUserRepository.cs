namespace re.colocar.me.talent.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    ProfileEntity Save(ProfileEntity entity);

    ProfileEntity? GetByUserName(string userName);

}