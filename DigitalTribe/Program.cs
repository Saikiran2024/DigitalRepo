using DTribe.Core.IRepositories;
using DTribe.Core.Mappings;
using DTribe.Core.Services;
using DTribe.DB;
using DTribe.DB.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{

    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        ConfigureMiddileware(builder);

        ConfigureDatabase(builder);

        ConfigureSwagger(builder);
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

        //ConfigureRequestPipelne(app);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
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
        builder.Services.AddAuthentication();
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

        builder.Services.AddScoped<IUserCategoryService, UserCategoryService>();
        builder.Services.AddScoped<IUserCategoriesRepository, UserCategoriesRepository>();

        builder.Services.AddScoped<ISectionRepository, SectionRepository>();
        builder.Services.AddScoped<ISectionService, SectionService>();

        builder.Services.AddScoped<ICategoriesService, CategoriesService>();
        builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
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

        app.UseAuthorization();

        app.MapControllers();
    }

    private static void RegisteredSingletoneServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}