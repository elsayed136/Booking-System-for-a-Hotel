using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Branch = new BranchRepository(db);
            RoomType = new RoomTypeRepository(db);
            Room = new RoomRepository(db);
            Reservation = new ReservationRepository(db);
        }
        public IBranchRepository Branch { get; set; }

        public IRoomTypeRepository RoomType { get; set; }
        public IRoomRepository Room { get; set; }
        public IReservationRepository Reservation { get; set; }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
