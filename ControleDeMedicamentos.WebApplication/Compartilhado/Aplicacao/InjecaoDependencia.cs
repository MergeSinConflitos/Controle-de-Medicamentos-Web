using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoEstoqueEntrada>();
        //services.AddScoped<ServicoEstoqueSaida>();
    }
}