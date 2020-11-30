using Surveynetic.Client.Core.Models;
using Surveynetic.Shared.DTO.Account;
using System.Threading.Tasks;

namespace Surveynetic.Client.Core.Interfaces.Services
{
    public interface ICurrentUserService
    {
        User User { get; }
        Task InitializeAsync();
        Task SignUpAsync(RegisterDto dto);
        Task SignInAsync(LoginDto dto);
        Task SignOutAsync();
    }
}
