using Backend.Business_Logic_Layer;
using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContexts(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDataAccessLayerServices(builder.Configuration);
builder.Services.AddBusinessAccessLayerServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// Seed data
// username : admin@test.com
// password : admin@test.com
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await using var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    using var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    if (await userManager.FindByNameAsync("admin@test.com") == null)
    {
        var adminUser = new ApplicationUser()
        {
            UserName = "admin@test.com",
            Email = "admin@test.com"
        };
        await userManager.CreateAsync(adminUser, "admin@test.com");
    }


}
app.Run();
