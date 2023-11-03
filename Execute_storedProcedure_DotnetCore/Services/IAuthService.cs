
using Execute_storedProcedure_DotnetCore.Models;

namespace Execute_storedProcedure_DotnetCore.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
