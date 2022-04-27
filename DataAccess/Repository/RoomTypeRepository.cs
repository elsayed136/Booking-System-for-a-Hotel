using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RoomType roomType)
        {
            _db.RoomTypes.Update(roomType);
        }


    }
}
