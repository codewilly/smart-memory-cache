namespace Cache.Interfaces
{
    public interface ICacheFacade
    {
        T? Get<T>(ICacheable<T> cacheable);

        T Create<T>(ICacheable<T> cacheable, T data, int? expiration = null, int? slidingExpiration = null);

        void Delete<T>(ICacheable<T> cacheable);
    }
}
