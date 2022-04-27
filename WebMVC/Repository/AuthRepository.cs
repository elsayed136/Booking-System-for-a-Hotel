using Newtonsoft.Json;
using System.Text;
using WebMVC.Models;
using WebMVC.Repository.IRepository;
using WebMVC.ViewModels;

namespace WebMVC.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthRepository(IHttpClientFactory clientFactory) 
        {
            _clientFactory = clientFactory;
        }

        public async Task<AuthModel> LoginAsync(string url, LoginVm objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new AuthModel();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthModel>(jsonString);
            }
            else
            {
                return new AuthModel();
            }
        }

        public async Task<AuthModel> RegisterAsync(string url, RegisterVm objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new AuthModel();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthModel>(jsonString);
            }
            else
            {
                return new AuthModel();
            }
        }
    }
}
