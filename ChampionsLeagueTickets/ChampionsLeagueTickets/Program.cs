using ChampionsLeagueTickets.Data;
using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interface;
using ChampionsLeagueTickets.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//DI
builder.Services.AddScoped<IDAO<Match>, MatchesDAO>();
builder.Services.AddScoped<IService<Match>, MatchesService>();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
Debug.WriteLine("CONNECTION STRING:");
Debug.WriteLine(cs);


try
{
    using var conn = new SqlConnection(cs);
    conn.Open();
    Debug.WriteLine("SQL CONNECTIE GELUKT");
}
catch (Exception ex)
{
    Debug.WriteLine("SQL FOUT:");
    Debug.WriteLine(ex.Message);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Info}/{action=Introductie}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
