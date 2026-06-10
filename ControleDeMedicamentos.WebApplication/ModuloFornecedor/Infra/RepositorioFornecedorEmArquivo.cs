using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Infra.Arquivo;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Infra;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedorEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> CarregarRegistros()
    {
        return contexto.Fornecedores;
    }
}
