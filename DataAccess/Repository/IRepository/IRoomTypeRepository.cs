using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IRoomTypeRepository : IBaseRepository<RoomType>
    {
        void Update(RoomType roomTypes);
    }
}
