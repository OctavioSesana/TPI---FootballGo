using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.WebAssembly;
using API.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient para la API (en WASM debe ser Scoped)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7074/")
});

// IAuthService también debe ser Scoped (no Singleton)
builder.Services.AddScoped<IAuthService, BlazorWasmAuthService>();

// Canchas API client
builder.Services.AddScoped<ICanchaClient, CanchaClient>();

var app = builder.Build();

// (Opcional) Si usás un proveedor estático para tus ApiClients:
var authService = app.Services.GetRequiredService<IAuthService>();
AuthServiceProvider.Register(authService);

await app.RunAsync();
