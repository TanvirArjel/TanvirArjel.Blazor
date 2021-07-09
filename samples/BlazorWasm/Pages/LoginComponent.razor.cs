using System.Threading.Tasks;
using BlazorWasm.Models;
using Microsoft.AspNetCore.Components.Forms;
using TanvirArjel.Blazor.Authorization;
using TanvirArjel.Blazor.Components;
using TanvirArjel.Blazor.Extensions;

namespace BlazorWasm.Pages
{
    public partial class LoginComponent
    {
        private readonly HostAuthStateProvider _hostAuthStateProvider;

        public LoginComponent(HostAuthStateProvider hostAuthStateProvider)
        {
            _hostAuthStateProvider = hostAuthStateProvider;
        }

        private EditContext EditContext { get; set; }

        private readonly LoginModel LoginModel = new LoginModel();

        private CustomValidationMessages ValidationMessages { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(LoginModel);
            EditContext.AddDataAnnotationsValidation();
            EditContext.AddBootstrapValidationClassProvider();
        }

        public async Task HandleValidSubmit()
        {
            string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRhbnZpcmFobWFkLmR1QGdtYWlsLmNvbSIsInN1YiI6IjEiLCJ1bmlxdWVfbmFtZSI6InRhbnZpcmFobWFkLmR1QGdtYWlsLmNvbSIsImp0aSI6IjA2MDZjZTBkLWIzZWUtNGI1Ni04Y2Y5LWVhZjMwNzEwNWMxMCIsImlhdCI6IjA3LzA5LzIwMjEgMTI6NTA6MTYiLCJuYmYiOjE2MjU4MzUwMTYsImV4cCI6MTYyNTkyMTQxNiwiaXNzIjoiZ2xvdHNhbG90LmNvbSIsImF1ZCI6Imdsb3RzYWxvdC5jb20ifQ.3Sa0h2ffOEnKR87cUJP8KL3n0WUpikMgHQCZeIUebA0";
            await _hostAuthStateProvider.LogInAsync(jwtToken);
        }

        public async Task LogoutAsync()
        {
            await _hostAuthStateProvider.LogOutAsync();
        }
    }
}
