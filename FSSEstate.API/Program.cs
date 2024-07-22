using FSSEstate.API.Authorization;
using FSSEstate.API.Extensions;
using FSSEstate.Business.Implementations;
using FSSEstate.Business.Interfaces;
using FSSEstate.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddHttpContextAccessor();
var services = builder.Services;
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// services.AddDbContext<AppDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionString"),
//         sqlServerOptions =>
//         {
//             sqlServerOptions.EnableRetryOnFailure(
//                 maxRetryCount: 10,
//                 maxRetryDelay: TimeSpan.FromSeconds(10),
//                 errorNumbersToAdd: null);
//             sqlServerOptions.MigrationsAssembly("FSSEstate.Repository");
//         });
// });
services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnectionString"));
});
services.AddRepositories()
    .AddServices()
    .AddScoped<IService, Services>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());

app.MapControllers();

app.Run();
