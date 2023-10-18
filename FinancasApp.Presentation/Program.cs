using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Services;
using FinancasApp.Infra.Data.Repositories;
using FinancasApp.Presentation.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//habilitando o uso de cookies no projeto
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//habilitando o uso de sessões no projeto
builder.Services.AddSession();

//habilitando autenticação por meio de cookies
builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

// Registrando as dependências do projeto
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IContaRepository, ContaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICategoriadomainService, CategoriaDomainService>();
builder.Services.AddTransient<IContaDomainService, ContaDomainService>();
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CacheControlFilter>();

app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();



