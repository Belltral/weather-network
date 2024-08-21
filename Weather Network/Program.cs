using WeatherNetwork.Cookies;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "LocalHostPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5500/home.html",
                "localhost:5500/home.html",
                "http://localhost:5500/",
                "localhost:5500/",
                "http://localhost:5500",
                "localhost:5500")
                .WithMethods("GET", "POST")
                .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IDailyWeatherService, DailyWeatherService>();
builder.Services.AddScoped<IHourlyWeatherService, HourlyWeatherService>();
builder.Services.AddScoped<ICookiesHandlerService, CookiesHandlerService>();

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportedCultures = new[] { "pt-BR", "en-US" };
    opt.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
});

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

app.UseCors("LocalHostPolicy");

app.UseAuthorization();

app.UseRequestLocalization();

app.MapControllerRoute(
    name: "DailyWeather",
    pattern: "DailyWeather",
    defaults: new { controller = "Daily", action = "Index"});

app.MapControllerRoute(
    name: "Weather",
    pattern: "Weather",
    defaults: new { controller = "Home", action = "GetWeather" });

app.MapControllerRoute(
    name: "Hourly",
    pattern: "ByHourly",
    defaults: new { controller = "Hourly", action = "Index" });
app.MapControllerRoute(
    name: "Hourly",
    pattern: "Hourly",
    defaults: new { controller = "Hourly", action = "GetHourly" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
