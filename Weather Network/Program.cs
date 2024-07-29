using WeatherNetwork.Mappings;
using WeatherNetwork.Services;
using WeatherNetwork.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("WeatherApi", opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUri:WeatherUri"]!);
});

builder.Services.AddScoped<IWeatherService, WeatherService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(CurrentWeatherProfile),
    typeof(HourlyProfile),
    typeof(DailyProfile),
    typeof(TodayWeatherProfile)
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); //"{controller=Home}/{action=Index}/{id?}");

app.Run();
