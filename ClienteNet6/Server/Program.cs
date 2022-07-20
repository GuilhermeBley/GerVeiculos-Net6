using ClienteNet6.Server.Context;
using ClienteNet6.Server.Filter;
using ClienteNet6.Server.Identity;
using ClienteNet6.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<LogAsyncActionFilter>();
    });
    builder.Services
        .AddRazorPages();

    #region Services

    builder.Services
        .AddSingleton<IConfiguration>(builder.Configuration)
        .AddScoped<ILocalStorageService, LocalStorageService>()
        .AddScoped<ITokenService, TokenService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IVeiculoService, VeiculoService>()
        .AddScoped<IInfracaoService, InfracaoService>()
        .AddScoped<ILogService, LogService>();

    #endregion

    #region Context

    builder.Services.AddDbContext<AppGerVeiculosContext>();

    #endregion

    #region Identity

    builder.Services.AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;

    })
    .AddEntityFrameworkStores<AppGerVeiculosContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>();


    #endregion

    #region TokenJwt

    var key = System.Text.Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(optionsBearer =>
            {
                optionsBearer.RequireHttpsMetadata = false;
                optionsBearer.SaveToken = true;
                optionsBearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        );
    #endregion
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages().RequireAuthorization();
    app.MapControllers().RequireAuthorization();
    app.MapFallbackToFile("index.html");

    app.Run();
}
