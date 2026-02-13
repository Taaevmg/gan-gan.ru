using System.Net;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5000); // HTTP на порту 5000
    // Для HTTPS добавьте аналогичную строку с настройкой сертификата:
    // serverOptions.Listen(IPAddress.Loopback, 5001, listenOptions => listenOptions.UseHttps());
});
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRewriter(new RewriteOptions()
    .AddRewrite("^test$", "html/test.html", skipRemainingRules: false)
    .AddRewrite("^login$", "html/login.html", skipRemainingRules: false)
);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


app.UseStaticFiles();
app.Run();
