namespace Libs.Adapters
{
    public interface IMapper<T, K>
    {
        T ToDomain(K raw);
    }
}

