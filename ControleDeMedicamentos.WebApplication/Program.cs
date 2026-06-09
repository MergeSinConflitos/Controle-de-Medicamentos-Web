using ControleDeMedicamentos.WebApplication.Compartilhado.Aplicacao;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices();

builder.Services.AddPresentation();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
