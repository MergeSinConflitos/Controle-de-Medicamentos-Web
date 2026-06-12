using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Dominio;

public class EstoqueSaida : EntidadeBase<EstoqueSaida>
{   
    public DateTime Data { get; set; } = DateTime.Now;
    public Paciente Paciente { get; set;} 
    public List<Medicamento> Medicamentos { get; set;}
    
    public EstoqueSaida(DateTime data, Paciente paciente, List<Medicamento> medicamentos)
    {   
        Data = data;
        Paciente = paciente;
        Medicamentos = medicamentos;
    }

    public EstoqueSaida()
    {
        
    }

    public override void Atualizar(EstoqueSaida entidadeAtualizada)
    {
        Paciente = entidadeAtualizada.Paciente;
        Medicamentos = entidadeAtualizada.Medicamentos;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default)
            erros.Add("O campo 'Data' é obrigatório");

        if (Paciente == null)
            erros.Add("O campo 'Paciente' é obrigatório");

        if (Medicamentos == null || Medicamentos.Count == 0)
            erros.Add("Selecione ao menos um medicamento");

        return erros;
    }
}
