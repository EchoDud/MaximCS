namespace MaximCS.API
{
    public interface IApiClient
    {
        Task<int> GetRandomNumberAsync(int maxValue);
    }
}
