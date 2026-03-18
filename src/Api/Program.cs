using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using Api.Extensions;
using Domain.TaskModelAggregate.Repositories;
using Persistence.Repositories;
using Persistence.UnitOfWorks;
using Api;
using Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddYamlFile(
    "appsettings.yml", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(
        Application.AssemblyReference.Assembly)); 

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseNpgsql(Env.POSTGRES_CONNECTION_STRING));

builder.Services.AddTransient<DefaultProjectSeed>();
builder.Services.AddTransient<ITaskModelRepository, TaskModelRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MigrateDbContext<TaskManagerDbContext>();

if (Env.USE_SEED_DATA)
    app.InstallSeed<DefaultProjectSeed>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/TaskModels/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TaskModels}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
