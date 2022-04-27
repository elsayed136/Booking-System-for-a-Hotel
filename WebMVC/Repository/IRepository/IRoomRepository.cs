using WebMVC.Models;

namespace WebMVC.Repository.IRepository
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRooms(string url, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Room>> ReportLike(string url, string token = "");
    }
}
