using GymManagementBLL;
using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region Dependency Injection
builder.Services.AddDbContext<GymContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // DefaultConnection is the name of the connection string in appsettings.json
});

builder.Services.AddScoped<DbContext, GymContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IMemberService, MemberServices>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
#endregion

#region AutoMapper Configuration
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
#endregion

var app = builder.Build();


#region Data Seeding
using (var scope = app.Services.CreateScope())
{
    var gymDbContext = scope.ServiceProvider.GetRequiredService<GymContext>();
    GymDataSeeding.SeedData(gymDbContext);
}
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
