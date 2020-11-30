using Microsoft.AspNetCore.Components;
using Surveynetic.Client.Core.Interfaces.Services;
using Surveynetic.Client.Core.Models;
using Surveynetic.Shared.DTO.Account;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Surveynetic.Client.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public User User { get; private set; }

        public CurrentUserService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task InitializeAsync()
        {
            User = await _localStorageService.GetItem<User>("user");
        }

        public async Task SignUpAsync(RegisterDto dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responce = await _httpClient.PostAsync("https://localhost:4001/account/signup", content);
            if (responce.IsSuccessStatusCode)
                _navigationManager.NavigateTo("/");

            throw new NotImplementedException();
        }

        public async Task SignInAsync(LoginDto dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var responce = await _httpClient.PostAsync("https://localhost:4001/account/signin", content);
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

            throw new NotImplementedException();
        }

        public async Task SignOutAsync()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("signin");
        }
    }
}
