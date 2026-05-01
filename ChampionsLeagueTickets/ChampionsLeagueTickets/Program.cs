using Azure.Identity;
using ChampionsLeagueTickets.Data;
using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

//Keyvault
var keyVaultUrl = builder.Configuration["KeyVault:Url"]
    ?? throw new InvalidOperationException("KeyVault URL ontbreekt");

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUrl),
    new DefaultAzureCredential()
);

//Services
var connectionString = builder.Configuration["DefaultConnection"] ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IAppEmailSender, EmailSender>();
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender>(
    sp => (Microsoft.AspNetCore.Identity.UI.Services.IEmailSender)sp.GetRequiredService<IAppEmailSender>()
);

builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

//Localization
builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("nl"),
        new CultureInfo("en"),
        new CultureInfo("fr")
    };

    options.DefaultRequestCulture = new RequestCulture("nl");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API voor Champions League Tickets applicatie",
        Version = "version 1",
        Description = "Een API om GET-requests uit te voeren op de Champions League Tickets applicatie. U moet ingelogd zijn om deze API te kunnen gebruiken (zie login endpoint).",
        Contact = new OpenApiContact
        {
            Name = "Ilona Defevere & Alina Korshenko",
            Email = "championsleagueticketsstudapp@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
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

//Seizoenen
builder.Services.AddScoped<ISeizoenenDAO, SeizoenenDAO>();
builder.Services.AddScoped<ISeizoenenService, SeizoenenService>();

//AbonnementenPrijs
builder.Services.AddScoped<IAbonementenPrijsDAO, AbonnementenPrijsDAO>();
builder.Services.AddScoped<IAbonementenPrijsService, AbonnementenPrijsService>();

//Tickets
builder.Services.AddScoped<ITicketDAO, TicketDAO>();
builder.Services.AddScoped<ITicketService, TicketService>();

//TicketPrijs
builder.Services.AddScoped<ITicketPrijsDAO, TicketPrijsDAO>();
builder.Services.AddScoped<ITicketPrijsService, TicketsPrijsService>();

//Abonementen
builder.Services.AddScoped<IAbonnementDAO, AbonnementenDAO>();
builder.Services.AddScoped<IAbonnementService, AbonnementenService>();

//Users
builder.Services.AddScoped<IUserService, UserService>();

//Orders
builder.Services.AddScoped<IOrderDAO, OrderDAO>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IDAO<Orderlijnen>, OrderlijnenDAO>();
builder.Services.AddScoped<IService<Orderlijnen>, OrderlijnenService>();

//Matches
builder.Services.AddScoped<IMatchDAO, MatchesDAO>();
builder.Services.AddScoped<IMatchService, MatchesService>();

//Tickets
builder.Services.AddScoped<ITicketDAO, TicketDAO>();
builder.Services.AddScoped<ITicketService, TicketService>();

//Hotel API
//builder.Services.AddScoped<IHotelService, HotelService>();

//Pdf
builder.Services.AddScoped<IPdfService, PdfService>();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.ChampionsLeagueTickets.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

//JWT en authenticatie
builder.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("User", policy =>
    {
        policy.RequireRole("User");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

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

//rollen
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(locOptions);

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Info}/{action=Introductie}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
