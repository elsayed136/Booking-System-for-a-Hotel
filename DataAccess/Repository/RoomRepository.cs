using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Room room)
        {
            _db.Rooms.Update(room);
        }
        public IEnumerable<Room> GetAvailableRooms(DateTime startTime, DateTime endTime)
        {
            IEnumerable<Room> allRooms = _db.Rooms.Include(r => r.Branch).Include(r => r.RoomType).ToList();

            var unavailable = _db.Reservations
                .Where(r =>
                    (r.StartDate > startTime && r.EndDate < endTime) ||
                    (r.StartDate <= endTime && r.EndDate > endTime) ||
                    (r.StartDate <= startTime && r.EndDate > startTime))
                .Select(r => r.RoomId);

            return allRooms.Where(r => !unavailable.Contains(r.Id)).ToList();
        }
        public IEnumerable<Room> ReportLike()
        {
            var startTime = DateTime.Now;
            var endTime = DateTime.Now.AddDays(1);
            IEnumerable<Room> available = GetAvailableRooms(startTime, endTime);

            IEnumerable<Room> allRooms = _db.Rooms.Include(r => r.Branch).Include(r => r.RoomType).ToList();
            var unavailable = _db.Reservations
                .Where(r =>
                    (r.StartDate > startTime && r.EndDate < endTime) ||
                    (r.StartDate <= endTime && r.EndDate > endTime) ||
                    (r.StartDate <= startTime && r.EndDate > startTime))
                .Select(r => r.RoomId);
            var unRoom = allRooms.Where(r => unavailable.Contains(r.Id)).ToList();
            foreach (var room in unRoom)
            {
                room.IsAvailable = false;
            }
            unRoom.AddRange(available);
            return unRoom;
        }
    }
}
