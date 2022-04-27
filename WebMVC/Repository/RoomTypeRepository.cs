using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Repository
{
    public class RoomTypeRepository : BaseRepository<RoomType> , IRoomTypeRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoomTypeRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
    }
}
