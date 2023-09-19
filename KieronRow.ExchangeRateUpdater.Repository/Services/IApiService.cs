public interface IApiService
{
    Task<T> CallAPI<T>(string url);
}