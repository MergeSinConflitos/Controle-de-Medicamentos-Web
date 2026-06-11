using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Infra;

namespace ControleDeMedicamentos.WebApplication.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmArquivo>();
    }
}