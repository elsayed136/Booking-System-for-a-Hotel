using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Repository
{
    public class BranchRepository : BaseRepository<Branch> , IBranchRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public BranchRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
    }
}
