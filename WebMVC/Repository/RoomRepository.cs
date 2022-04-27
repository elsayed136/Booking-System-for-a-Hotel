using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Repository
{
    public class RoomRepository : BaseRepository<Room> , IRoomRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoomRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<Room>> GetAvailableRooms(string url, DateTime startDate, DateTime endDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + "Search/"+ $"?StartDate={startDate}&EndDate={endDate}");

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Room>>(jsonString);
            }

            return null;
        }

        public async Task<IEnumerable<Room>> ReportLike(string url, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + "ReportPage/");

            var client = _clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Room>>(jsonString);
            }

            return null;
        }

    }
}
