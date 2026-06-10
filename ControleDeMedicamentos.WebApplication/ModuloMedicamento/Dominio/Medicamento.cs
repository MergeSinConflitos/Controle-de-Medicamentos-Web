using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEmEstoque { get; set; } = 0;
    public Fornecedor Fornecedor { get; set; }

    public Medicamento(string nome, string descricao, Fornecedor fornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        Fornecedor = fornecedor;
    }

    public Medicamento()
    {

    }

    public override void Atualizar(Medicamento entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        this.Nome = medicamentoAtualizado.Nome;
        this.Descricao = medicamentoAtualizado.Descricao;
        this.QuantidadeEmEstoque = medicamentoAtualizado.QuantidadeEmEstoque;
        this.Fornecedor = medicamentoAtualizado.Fornecedor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add("O campo '/Nome/' é obrigatório");
        }
        else if (Nome.Length < 3 || Nome.Length > 100)
        {
            erros.Add("O nome deve ter entre 3 e 100 caracteres");
        }

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            erros.Add("O campo '/Descrição/' é obrigatório");
        }
        else if (Descricao.Length < 5 || Descricao.Length > 255)
        {
            erros.Add("A descrição deve ter entre 5 e 255 caracteres");
        }

        if (QuantidadeEmEstoque < 0)
        {
            erros.Add("Informe um número positivo");
        }

        return erros;
    }
}
