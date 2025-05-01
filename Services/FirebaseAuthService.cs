namespace PPMGChronical.Services
{
    // Services/FirebaseAuthService.cs
    using Microsoft.JSInterop;
    using PPMGChronical.Models;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class FirebaseAuthService
    {
        private readonly IJSRuntime _jsRuntime;

        public FirebaseAuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<AuthResult> RegisterUserAsync(string email, string password)
        {
            return await _jsRuntime.InvokeAsync<AuthResult>("firebaseInterop.registerUser", email, password);
        }

        public async Task<AuthResult> SignInAsync(string email, string password)
        {
            return await _jsRuntime.InvokeAsync<AuthResult>("firebaseInterop.signIn", email, password);
        }

        public async Task<AuthResult> SignOutAsync()
        {
            var result = await _jsRuntime.InvokeAsync<AuthResult>("firebaseInterop.signOut");
            return result;
        }

        public async Task<UserData> GetCurrentUserAsync()
        {
            var result = await _jsRuntime.InvokeAsync<dynamic>("firebaseInterop.getCurrentUser");

            if (((JsonElement)result).GetProperty("isAuthenticated").GetBoolean())
            {
                return new UserData
                {
                    Uid = ((JsonElement)result).GetProperty("uid").GetString(),
                    Email = ((JsonElement)result).GetProperty("email").GetString()
                };
            }

            return null;
        }
    }
}
