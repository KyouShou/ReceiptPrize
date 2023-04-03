using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ReceiptPrize.Repository;
using ReceiptPrize.Service;

var builder = WebApplication.CreateBuilder(args);

var cache = new MemoryCache(new MemoryCacheOptions()
{
    ExpirationScanFrequency = TimeSpan.FromMinutes(5)
});

builder.Services.AddTransient<IMemoryCache, MemoryCache>(c => cache);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICheckPrizeService>(service =>
    new CheckPrizeService(
    new FetchPrizeNumService(
    new MinistryOfFinancePrizeNumRepository(
    )) 
    , cache));



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
