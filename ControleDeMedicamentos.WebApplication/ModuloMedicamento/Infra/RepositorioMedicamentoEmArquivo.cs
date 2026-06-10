using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Infra;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> CarregarRegistros()
    {
        return contexto.Medicamentos;
    }
}
