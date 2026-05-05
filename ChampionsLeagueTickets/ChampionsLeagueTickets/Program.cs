using Azure.Identity;
using ChampionsLeagueTickets.Data;
using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services;
using ChampionsLeagueTickets.Services.Interfaces;
using ChampionsLeagueTickets.Services.Mail;
using ChampionsLeagueTickets.Services.Mail.Interfaces;
using ChampionsLeagueTickets.Services.Pdf;
using ChampionsLeagueTickets.Services.Pdf.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//keyvault
var keyVaultUrl = builder.Configuration["KeyVault:Url"];

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUrl!),
    new DefaultAzureCredential()
);

//database
var defaultConnection = builder.Configuration["DefaultConnection"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(defaultConnection));

builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(defaultConnection));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

//jwt authentication
var jwtIssuer = builder.Configuration["JwtIssuer"];
var jwtAudience = builder.Configuration["JwtAudience"];
var jwtKey = builder.Configuration["JwtKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!)
        ),

        ClockSkew = TimeSpan.Zero
    };
});

//authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

//cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

//email
builder.Services.AddTransient<IAppEmailSender, EmailSender>();
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender>(
    sp => (Microsoft.AspNetCore.Identity.UI.Services.IEmailSender)
        sp.GetRequiredService<IAppEmailSender>()
);

//localization
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

//swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Champions League Tickets",
        Version = "v1",
        Description = "API voor Champions League Tickets applicatie",
        Contact = new OpenApiContact
        {
            Name = "Ilona & Alina",
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
        Description = "Enter: Bearer {token}"
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

//auto mapper
builder.Services.AddAutoMapper(typeof(Program));

//DI
//stadion
builder.Services.AddScoped<IDAO<Stadion>, StadionDAO>();
builder.Services.AddScoped<IService<Stadion>, StadionService>();

//matches
builder.Services.AddScoped<IMatchDAO, MatchesDAO>();
builder.Services.AddScoped<IMatchService, MatchesService>();

//vaktypes
builder.Services.AddScoped<IDAO<VakType>, VakTypeDAO>();
builder.Services.AddScoped<IService<VakType>, VakTypeService>();

//zitplaatsen
builder.Services.AddScoped<IZitplaatsenDAO, ZitplaatsenDAO>();
builder.Services.AddScoped<IZitplaatsenService, ZitplaatsenService>();

//seizoenen
builder.Services.AddScoped<IDAO<Seizoenen>, SeizoenenDAO>();
builder.Services.AddScoped<IService<Seizoenen>, SeizoenenService>();

//abonnementen
builder.Services.AddScoped<IAbonnementDAO, AbonnementenDAO>();
builder.Services.AddScoped<IAbonnementService, AbonnementenService>();

builder.Services.AddScoped<IAbonementenPrijsDAO, AbonnementenPrijsDAO>();
builder.Services.AddScoped<IAbonementenPrijsService, AbonnementenPrijsService>();

//tickets
builder.Services.AddScoped<ITicketDAO, TicketDAO>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<ITicketPrijsDAO, TicketPrijsDAO>();
builder.Services.AddScoped<ITicketPrijsService, TicketsPrijsService>();

//orders
builder.Services.AddScoped<IOrderDAO, OrderDAO>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderLijnenDAO, OrderLijnenDAO>();
builder.Services.AddScoped<IOrderLijnenService, OrderLijnenService>();

//gebruikers identity
builder.Services.AddScoped<IUserService, UserService>();

//hotels api
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddHttpClient<HotelService>();

//pdf service
builder.Services.AddScoped<IPdfService, PdfService>();

//session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.ChampionsLeagueTickets.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

//debug connection check
Debug.WriteLine("CONNECTION STRING:");
Debug.WriteLine(defaultConnection);

try
{
    using var conn = new SqlConnection(defaultConnection);
    conn.Open();
    Debug.WriteLine("SQL CONNECTIE GELUKT");
}
catch (Exception ex)
{
    Debug.WriteLine("SQL FOUT:");
    Debug.WriteLine(ex.Message);
}

//build
var app = builder.Build();

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

var locOptions = app.Services
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

app.UseRequestLocalization(locOptions);

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Info}/{action=Introductie}/{id?}")
    .WithStaticAssets();

app.MapRazorPages().WithStaticAssets();

app.Run();