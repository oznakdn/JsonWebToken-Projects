using Jwt.Api.GlobalException;
using Jwt.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
                .AddSwagger()
                .AddDBContext(builder.Configuration)
                .AddAuthenticationAuthorization(builder.Configuration)
                .AddServiceContainer()
                .AddMapper()
                .AddFluentValidation()
                .AddCorsPolicy()
                .AddSignalR();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
}

app.UseCors("default");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ExceptionHandler>();
app.MapControllers();
app.MapHub<ProductHub>("/producthub");


app.Run();
