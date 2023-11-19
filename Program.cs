using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); ;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5174",
                                              "http://localhost:5173");
                      });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();  // Log to the console
    builder.AddDebug();    // Log to the debug output
                           // Add other log providers as needed
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapCustModelEndpoints();

app.MapCustomerModelEndpoints();

app.MapProductModelEndpoints();

app.MapSaleModelEndpoints();

app.MapStoreModelEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Title V1");
});

app.Run();

