using DataAccess.Repository.IRepository;
using Models;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Any(Expression<Func<Reservation, bool>> fillter)
        {
            return _db.Reservations.Any(fillter);
        }

        public void Update(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
        }
    }
}
