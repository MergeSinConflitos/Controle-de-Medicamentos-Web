using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFuncionario>();
    }
}