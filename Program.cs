using Microsoft.EntityFrameworkCore;
using re.colocar.me.talent.Domain;
using re.colocar.me.talent.Domain.Interfaces.Infrastructure;
using re.colocar.me.talent.Domain.Interfaces.Repositories;
using re.colocar.me.talent.Domain.Interfaces.Services;
using re.colocar.me.talent.Infrastructure;
using re.colocar.me.talent.Infrastructure.Repositories;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddEnvironmentVariables().Build();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();

builder.Services.AddOptions();

builder.Services.Configure<LinkedinConfigurations>(configuration.GetSection(LinkedinConfigurations.LinkedinOptions));
builder.Services.Configure<FacebookConfigurations>(configuration.GetSection(FacebookConfigurations.FacebookOptions));


// Add services to the container.
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILinkedinClient, LinkedinClient>();
builder.Services.AddTransient<IFacebookClient, FacebookClient>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<TalentDbContext>((DbContextOptionsBuilder options) =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default") ?? string.Empty);
}
);

builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors((option) =>
{
    option.AllowAnyOrigin();
    option.AllowAnyHeader();
    option.AllowAnyMethod();
});

app.UseSerilogRequestLogging();

app.Run();
