using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Infra;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorio<Funcionario>
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> CarregarRegistros()
    {
        return contexto.Funcionario;
    }
}
