using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{

    public interface IUnitOfWork : IDisposable
    {
        IBranchRepository Branch { get; }
        IRoomTypeRepository RoomType { get; }
        IRoomRepository Room { get; }
        IReservationRepository Reservation { get; }
        bool Save();
    }
}
