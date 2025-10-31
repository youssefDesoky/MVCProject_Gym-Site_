using System.Data.Common;
using GymManagementBLL;
using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion

#region Identity Configuration
// This Is Default Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<GymContext>(); // <-- Add The Database Context Here

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // This is Default
    options.AccessDeniedPath = "/Account/AccessDenied";
});
#endregion

#region AutoMapper Configuration
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
#endregion

var app = builder.Build();

app.UseStaticFiles(); // <-- required to serve wwwroot files

#region Data Seeding
using (var scope = app.Services.CreateScope())
{
    var gymDbContext = scope.ServiceProvider.GetRequiredService<GymContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    IdentityDataSeed.SeedData(roleManager, userManager);
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

app.UseAuthentication(); // <-- Add Authentication Middleware [Must be before Authorization]
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
