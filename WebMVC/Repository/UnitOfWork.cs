using WebMVC.Repository.IRepository;

namespace WebMVC.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IHttpClientFactory _clientFactory;
        public UnitOfWork(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            Branch = new BranchRepository(clientFactory);
            RoomType = new RoomTypeRepository(clientFactory);
            Room = new RoomRepository(clientFactory);
            Reservation = new ReservationRepository(clientFactory);
        }
        public IBranchRepository Branch { get; set; }
        public IRoomTypeRepository RoomType { get; set; }
        public IRoomRepository Room { get; set; }
        public IReservationRepository Reservation { get; set; }
    }
}
