

using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Dominio;

public class EstoqueEntrada : EntidadeBase<EstoqueEntrada>
{
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }

    public EstoqueEntrada(Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public EstoqueEntrada()
    {

    }

    public void RegistrarEntrada()
    {
        if (Medicamento == null)
        {
            return;
        }
        if (Quantidade <= 0)
        {
            return;
        }
        Medicamento.QuantidadeEmEstoque += Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Funcionario == null)
        {
            erros.Add("Funcionario invalido");
        }

        if (Medicamento == null)
        {
            erros.Add("Medicamento invalido");
        }

        if (Quantidade < 0)
        {
            erros.Add("Informe um número positivo");
        }

        return erros;
    }

    public override void Atualizar(EstoqueEntrada entidadeAtualizada)
    {
        Medicamento = entidadeAtualizada.Medicamento;
        Funcionario = entidadeAtualizada.Funcionario;
        Quantidade = entidadeAtualizada.Quantidade;
    }
}
