using Jwt.WebMvc.ClientServices.Concretes;
using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
//    options =>
//{
//    options.Filters.Add(new AuthorizationFilter());
//});
builder.Services.AddHttpClient("Auth", conf => conf.BaseAddress = new Uri("https://localhost:7044/api"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(1)); // session expire time



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
