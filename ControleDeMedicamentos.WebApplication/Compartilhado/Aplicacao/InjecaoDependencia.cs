using System;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoPaciente>();
    }
}