using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionario.Infra;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto) {}

    protected override List<Funcionario> CarregarRegistros()
    {
        return contexto.funcionario;
    }
}
