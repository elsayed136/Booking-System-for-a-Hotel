namespace WebMVC.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetFirstOrDefaultAsync(string url, int Id, string token);
        Task<IEnumerable<T>> GetAllAsync(string url, string token);
        Task<bool> AddAsync(string url, T objToCreate, string token);
        Task<bool> UpdateAsync(string url, T objToUpdate, string token);
        Task<bool> RemoveAsync(string url, int Id, string token);
    }
}
