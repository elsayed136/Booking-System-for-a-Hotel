using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        void Update(Room room);
        IEnumerable<Room> GetAvailableRooms(DateTime startTime, DateTime endTime);
        IEnumerable<Room> ReportLike();
    }
}
