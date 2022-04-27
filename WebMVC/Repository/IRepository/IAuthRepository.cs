using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<AuthModel> LoginAsync(string url, LoginVm objToCreate);
        Task<AuthModel> RegisterAsync(string url, RegisterVm objToCreate);
    }
}
