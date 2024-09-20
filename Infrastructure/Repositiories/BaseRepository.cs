public abstract class BaseRepository<T>
    where T : BaseEntity
{
    public abstract IEnumerable<T>? ListAll();
    public abstract T? Get(Guid Id);
    public abstract T Save(T entity);
    public abstract void Delete(Guid Id);
}