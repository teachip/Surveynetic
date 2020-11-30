using Surveynetic.Client.Core.Models;
using Surveynetic.Shared.DTO.Account;
using System.Threading.Tasks;

namespace Surveynetic.Client.Core.Interfaces.Services
{
    public interface IUserService
    {
        User User { get; }
        Task InitializeAsync();
        Task LoginAsync(LoginDto dto);
        Task LogoutAsync();
    }
}
