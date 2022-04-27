using Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        void Update(Reservation reservation);
        bool Any(Expression<Func<Reservation, bool>> fillter); 
    }
}
