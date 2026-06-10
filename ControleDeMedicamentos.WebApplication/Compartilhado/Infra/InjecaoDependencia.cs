using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Infra;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Infra;

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

        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmArquivo>();
        services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmArquivo>();
    }
}