using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Reflection;
using VitourProjectCase.Services.CategoryServices;
using VitourProjectCase.Services.EmailServices;
using VitourProjectCase.Services.GalleryServices;
using VitourProjectCase.Services.ReportServices;
using VitourProjectCase.Services.ReservationServices;
using VitourProjectCase.Services.ReviewServices;
using VitourProjectCase.Services.TourPlanServices;
using VitourProjectCase.Services.TourServices;
using VitourProjectCase.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ITourPlanService, TourPlanService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationValidator, ReservationValidator>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingKey"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


var app = builder.Build();

var supportedCultures = new[] { "en", "tr"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
