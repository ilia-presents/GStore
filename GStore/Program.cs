using GStore.Data;
using GStore.Repositories;
using GStore.Repositories.Interfaces;
using GStore.Utils.ImageDataHelper;
using GStore.Utils.ImageDataHelper.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddTransient<ILevel2CategoryRepo, Level2CategoryRepo>();
builder.Services.AddTransient<IShirtRepo, ShirtRepo>();
builder.Services.AddTransient<ISizeSetRepo, SizeSetRepo>();
builder.Services.AddTransient<IColorSetRepo, ColorSetRepo>();
builder.Services.AddTransient<IShirtColorSetRepo, ShirtColorSetRepo>();
builder.Services.AddTransient<IShirtSizeSetRepo, ShirtSizeSetRepo>();
builder.Services.AddTransient<IShirtAvailabalityRepo, ShirtAvailabalityRepo>();

builder.Services.AddTransient<IImageManager, ImageManager>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();

app.Run();
