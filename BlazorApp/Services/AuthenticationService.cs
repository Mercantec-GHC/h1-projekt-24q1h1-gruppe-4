using Domain_Models; 
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class AuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DatabaseService _databaseService;

        public AuthenticationService(IJSRuntime jsRuntime, DatabaseService databaseService)
        {
            _jsRuntime = jsRuntime;
            _databaseService = databaseService;
        }

        public async Task Initialize()
        {
            await LoadFromLocalStorage();
            ClearLocalStorageOnPageExit();
        }

        public bool IsAuthenticated { get; private set; }
        public User CurrentUser { get; private set; }

        public async Task Login(string username, string password)
        {
            if (_databaseService.ValidateUser(username, password))
            {
                var user = _databaseService.GetUserByUsername(username);
                IsAuthenticated = true;
                CurrentUser = user;
                await SaveToLocalStorage();
            }
        }

        public async Task Logout()
        {
            IsAuthenticated = false;
            CurrentUser = null;
            await ClearLocalStorage();
        }

        private async Task SaveToLocalStorage()
        {
            if (CurrentUser != null)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IsAuthenticated", IsAuthenticated.ToString());
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "CurrentUser_Username", CurrentUser.Username);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "CurrentUser_UserID", CurrentUser.UserID.ToString());
            }
        }

        public async Task LoadFromLocalStorage()
        {
            string isAuthenticatedString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "IsAuthenticated");
            if (!string.IsNullOrEmpty(isAuthenticatedString))
            {
                IsAuthenticated = bool.Parse(isAuthenticatedString);
            }

            string username = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "CurrentUser_Username");
            string userIdString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "CurrentUser_UserID");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(userIdString))
            {
                int userId = int.Parse(userIdString);
                CurrentUser = new User { Username = username, UserID = userId };
            }
        }


        private async Task ClearLocalStorage()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "IsAuthenticated");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "CurrentUser_Username");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "CurrentUser_UserID");
        }

        private async Task ClearLocalStorageOnPageExit()
        {
            await _jsRuntime.InvokeVoidAsync("eval", @"
                window.addEventListener('beforeunload', function() {
                    localStorage.clear();
                });
            ");
        }
    }
}




