using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONFIGURAÇÃO DE SERVIÇOS (O que o app TEM)
// ==========================================

// IMPORTANTE: Adiciona suporte a Controllers e Views (CSHTML)
builder.Services.AddControllersWithViews(); 

builder.Services.AddOpenApi();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

// ==========================================
// 2. CONFIGURAÇÃO DO PIPELINE (O que o app FAZ)
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Necessário se você tiver arquivos CSS/JS na pasta wwwroot
app.UseStaticFiles(); 

// A ORDEM AQUI É CRÍTICA:
app.UseAuthentication(); // 1º: Quem é você?
app.UseAuthorization();  // 2º: Você pode entrar?

// Mapeia os controllers para que as rotas (ex: /login) funcionem
app.MapControllers(); 

 

app.Run();