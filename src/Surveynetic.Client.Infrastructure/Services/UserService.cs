using Microsoft.AspNetCore.Components;
using Surveynetic.Client.Core.Interfaces.Services;
using Surveynetic.Client.Core.Models;
using Surveynetic.Shared.DTO.Account;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Surveynetic.Client.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public User User { get; private set; }

        public UserService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task InitializeAsync()
        {
            User = await _localStorageService.GetItem<User>("user");
        }

        public async Task LoginAsync(LoginDto dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responce = await _httpClient.PostAsync("https://localhost:44385/account/login", content);
            if (responce.IsSuccessStatusCode)
            {
                var responceContent = await responce.Content.ReadAsStringAsync();
                var userDto = JsonSerializer.Deserialize<AuthDto>(responceContent);
                User = new User
                {
                    UserName = userDto.UserName,
                    Token = userDto.Token
                };

                await _localStorageService.SetItem("user", User);
                _navigationManager.NavigateTo("/");
            }
        }

        public async Task LogoutAsync()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
