using Jwt.Blazor;
using Jwt.Blazor.Services.Concretes;
using Jwt.Blazor.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7044/api/") });
builder.Services.AddScoped<IProductService,ProductService>();

await builder.Build().RunAsync();
