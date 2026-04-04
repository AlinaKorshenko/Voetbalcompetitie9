using ChampionsLeagueTickets.Data;
using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//Services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API for Champions League Tickets application",
        Version = "version 1",
        Description = "An API to perform Get operations on Champions League Tickets information",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "ID",
            Email = "ilona.defevere@student.vives.be",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "ChampionsLeague API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//DI
//Stadion
builder.Services.AddScoped<IDAO<Stadion>, StadionDAO>();
builder.Services.AddScoped<IService<Stadion>, StadionService>();

//VakType
builder.Services.AddScoped<IDAO<VakType>, VakTypeDAO>();
builder.Services.AddScoped<IService<VakType>, VakTypeService>();

//Zitplaatsen
builder.Services.AddScoped<IZitplaatsenDAO, ZitplaatsenDAO>();
builder.Services.AddScoped<IZitplaatsenService, ZitplaatsenService>();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//DI
builder.Services.AddScoped<IMatchDAO, MatchesDAO>();
builder.Services.AddScoped<IMatchService, MatchesService>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Info}/{action=Introductie}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
