using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Infra;

public class RepositorioEstoqueSaidaEmArquivo : RepositorioBaseEmArquivo<EstoqueSaida> , IRepositorioEstoqueSaida
{
    public RepositorioEstoqueSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<EstoqueSaida> CarregarRegistros()
    {
        return contexto.EstoqueSaida;
    }
}   
