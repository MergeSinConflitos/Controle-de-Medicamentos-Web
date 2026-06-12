using System;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Aplicacao;

public class ServicoEstoqueSaida
{
    private readonly IRepositorioEstoqueSaida repositorioEstoqueSaida;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoEstoqueSaida(
        IRepositorioEstoqueSaida repositorioEstoqueSaida,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioEstoqueSaida = repositorioEstoqueSaida;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

   public Result Cadastrar(CadastrarEstoqueSaidaDto dto)
    {
        Result<(Paciente Paciente, Medicamento Medicamento)> resultadoRelacionamentos =
            SelecionarRelacionamentos(dto.PacienteId, dto.MedicamentoId);

        if (resultadoRelacionamentos.IsFailed)
            return Result.Fail(resultadoRelacionamentos.Errors);

        Paciente paciente = resultadoRelacionamentos.Value.Paciente;
        Medicamento medicamento = resultadoRelacionamentos.Value.Medicamento;

        
        if (medicamento.QuantidadeEmEstoque < dto.Quantidade)
            return Falha(nameof(dto.Quantidade),
                $"Estoque insuficiente. Disponível: {medicamento.QuantidadeEmEstoque}");

        EstoqueSaida novaSaida = new EstoqueSaida(medicamento, paciente, dto.Quantidade);

        Result resultadoValidacao = ValidarEntidade(novaSaida);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        
        medicamento.QuantidadeEmEstoque -= dto.Quantidade;

        repositorioEstoqueSaida.Cadastrar(novaSaida);

        repositorioMedicamento.Editar(dto.MedicamentoId, medicamento);

        return Result.Ok().WithSuccess("Requisição de saída registrada com sucesso");
    }

    public Result<GerenciarEstoqueSaidaDto> SelecionarTodosPorPaciente(Guid pacienteId)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(pacienteId);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        List<ListarEstoqueSaidaDto> saidas = repositorioEstoqueSaida
            .Filtrar(s => s.Paciente.Id == pacienteId)
            .Select(MapearParaListarDto)
            .ToList();

        return Result.Ok(new GerenciarEstoqueSaidaDto(
            paciente.Id,
            paciente.Nome,
            saidas
        ));
    }

    public List<OpcaoMedicamentoDto> SelecionarMedicamentos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Where(m => m.QuantidadeEmEstoque > 0) 
            .Select(m => new OpcaoMedicamentoDto(m.Id, m.Nome, m.QuantidadeEmEstoque))
            .ToList();
    }

    private Result<(Paciente Paciente, Medicamento Medicamento)> SelecionarRelacionamentos(
        Guid pacienteId,
        Guid medicamentoId)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(pacienteId);

        if (paciente == null)
            return Result.Fail(new Error("Selecione um paciente válido.")
                .WithMetadata("Campo", nameof(pacienteId)));

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(medicamentoId);

        if (medicamento == null)
            return Result.Fail(new Error("Selecione um medicamento válido.")
                .WithMetadata("Campo", nameof(medicamentoId)));

        return Result.Ok((paciente, medicamento));
    }

    private static Result ValidarEntidade(EstoqueSaida estoqueSaida)
    {
        List<string> erros = estoqueSaida.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static ListarEstoqueSaidaDto MapearParaListarDto(EstoqueSaida estoque)
    {
        return new ListarEstoqueSaidaDto(
            estoque.Id,
            estoque.Data,
            estoque.Medicamento.Id,
            estoque.Medicamento.Nome,
            estoque.Medicamento.QuantidadeEmEstoque,
            estoque.Quantidade
        );
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
