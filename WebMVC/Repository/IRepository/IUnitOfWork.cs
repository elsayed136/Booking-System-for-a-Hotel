namespace WebMVC.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBranchRepository Branch { get; }
        IRoomTypeRepository RoomType { get; }
        IRoomRepository Room { get; }
        IReservationRepository Reservation { get; }
    }
}
