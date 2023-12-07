using FieldFinder.BLL.Interfaces;
using FieldFinder.BLL.Repositories;
using FieldFinder.DAL.Context;
using FieldFinder.PL.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Adding Singltone for ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));

//Allow Dependency Injection for autoMapper
builder.Services.AddAutoMapper(m => m.AddProfile(new FieldProfile()));

// Add Scoped for DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//allow Di for scope
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
