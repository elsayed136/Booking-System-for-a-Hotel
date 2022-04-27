using Models.Dtos;

namespace DataAccess.Repository.IRepository
{
    public interface IAuthRepository 
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);

    }
}
