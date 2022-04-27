using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ReservationRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

    }
}
