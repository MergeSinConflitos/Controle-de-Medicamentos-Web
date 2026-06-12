using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Dominio;

public class EstoqueSaida : EntidadeBase<EstoqueSaida>
{   
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento Medicamento { get; set; } = null!;
    public Paciente Paciente { get; set; } = null!;
    public int Quantidade { get; set; }

    public EstoqueSaida() { }

    public EstoqueSaida(Medicamento medicamento, Paciente paciente, int quantidade)
    {
        Medicamento = medicamento;
        Paciente = paciente;
        Quantidade = quantidade;
        Data = DateTime.Now;
    }

    public override void Atualizar(EstoqueSaida entidadeAtualizada)
    {
        Medicamento = entidadeAtualizada.Medicamento;
        Paciente = entidadeAtualizada.Paciente;
        Quantidade = entidadeAtualizada.Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" é obrigatório");

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" é obrigatório");

        if (Quantidade <= 0)
            erros.Add("A quantidade deve ser maior que zero");

        return erros;
    }
}
