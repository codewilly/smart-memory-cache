namespace Cache.Interfaces
{
    public interface ICacheable<TResult>
    {
        string GetHashKey();

        TResult? GetDefaultValue();
    }
}
