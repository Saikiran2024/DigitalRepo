using DTribe.Core.IRepositories;
using DTribe.Core.Mappings;
using DTribe.Core.Services;
using DTribe.Core.Services.Auth;
using DTribe.DB;
using DTribe.DB.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{

    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        ConfigureMiddileware(builder);

        ConfigureDatabase(builder);

        ConfigureSwagger(builder);

        RegisterJwtAuthentication(builder); // Register JWT Authentication

        // Add services to the container.


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //builder.Services.AddScoped<ICategoryService, CategoryService>();
        //builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        //builder.Services.AddDbContextFactory<ApplicationDbContext>();

        RegisteredSingletoneServices(builder);

        RegisterScoppedServices(builder);

        var app = builder.Build();

        ConfigureRequestPipelne(app);

        //ConfigureRequestPipelne(app);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles(); // This enables serving static files from wwwroot

        app.UseAuthorization();
        app.UseAuthentication(); // Add authentication middleware
        app.MapControllers();

        app.Run();


        //app.UseMiddleware<TokenValidationMiddleware>();

       
    }
    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
    //    DtribeDBConnectionStringProvider.Initilize(builder.Configuration);
    //    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    //DtribeDBConnectionStringProvider.GetConnectionString(),
    //sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

        DtribeDBConnectionStringProvider.Initilize(builder.Configuration);
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DtribeDBConnectionStringProvider.GetConnectionString()));
    }

    private static void ConfigureMiddileware(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        //builder.Services.AddAuthentication();
        builder.Services.AddMemoryCache();
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void RegisterScoppedServices(WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<ITokenValidationService, DatabaseTokenValidationService>();


        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IStorageService,StorageService>();
        builder.Services.AddScoped<IUserCategoryService, UserCategoryService>();
        builder.Services.AddScoped<IUserCategoriesRepository, UserCategoriesRepository>();

        builder.Services.AddScoped<ISectionRepository, SectionRepository>();
        builder.Services.AddScoped<ISectionService, SectionService>();

        builder.Services.AddScoped<ICategoriesService, CategoriesService>();
        builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

        builder.Services.AddScoped<IUserInfoService, UserInfoServices>();
        builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        //builder.Services.AddDbContextFactory<ApplicationDbContext>();
    }

    private static void ConfigureRequestPipelne(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseMiddleware<TokenValidationMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication(); // Add authentication middleware
        app.UseAuthorization();
       

        app.MapControllers();
    }

    private static void RegisteredSingletoneServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


    }
    private static void RegisterJwtAuthentication(WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]); // Ensure this is a strong key and secure it

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }
}