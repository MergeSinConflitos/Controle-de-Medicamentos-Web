using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Infra;

public class RepositorioEstoqueEntradaEmArquivo : RepositorioBaseEmArquivo<EstoqueEntrada>, IRepositorioEstoqueEntrada
{
    public RepositorioEstoqueEntradaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<EstoqueEntrada> CarregarRegistros()
    {
        return contexto.EstoqueEntrada;
    }

}