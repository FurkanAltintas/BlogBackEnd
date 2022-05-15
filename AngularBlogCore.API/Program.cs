using AngularBlogCore.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AngularBlogDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnectionString"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(configure => configure
        // .AllowAnyOrigin() // Bana hangi siteden gelirsen gel buradaki apilere eriþimin var
        .AllowAnyMethod() // Buradaki endpointlere ister get, ister put, ister delete http method tipleriyle istek atabilirsin
        .AllowAnyHeader() // Buradaki endpointlere istek attýðýnda headerýnda ne olursa olsun gel. Her hangi bir header deðerine sahip olabilirsin
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // Herhangi bir kimlik doðrulama ile girebilirsin);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(configure => configure.MapControllers());

app.Run();
