using System;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        if (ExistePacienteComMesmoCartao(dto.CartaoSus))
        {
            return Falha("CartaoSus", "Já existe um paciente com esse Cartão do Sus");
        }

        Paciente novopaciente = new Paciente(
            dto.Nome,
            dto.Telefone,
            dto.CartaoSus,
            dto.Cpf
        );

        Result resultadoValidacao = ValidarEntidade(novopaciente);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        repositorioPaciente.Cadastrar(novopaciente);
        return Result.Ok().WithSuccess("Paciente cadastrado com sucesso");
    }

    public Result Editar(EditarPacienteDto dto)
    {
        if (ExistePacienteComMesmoCartao(dto.CartaoSus, dto.Id))
        {
            return Falha("CartaoSus", "Já existe um paciente com esse Cartão do Sus");
        }

        Paciente pacienteAtualizado = new Paciente(
            dto.Nome,
            dto.Telefone,
            dto.CartaoSus,
            dto.Cpf
        );

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        bool conseguiuEditar = repositorioPaciente.Editar(dto.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Paciente não encontrado.");


        return Result.Ok().WithSuccess("Paciente editado com sucesso");
    }

    public Result Excluir(Guid Id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(Id);

        if (paciente == null)
        {
            return Result.Fail("Paciente não encontrado");
        }

        repositorioPaciente.Excluir(Id);

        return Result.Ok().WithSuccess("Paciente excluido com sucesso");
    }

    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente.SelecionarTodos()
        .Select(p => new ListarPacientesDto(
            p.Id,
            p.Nome,
            p.Telefone,
            p.CartaoSus,
            p.Cpf)).ToList();
    }

    public Result<DetalhesPacienteDto> SelecionarPorId(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        return Result.Ok(new DetalhesPacienteDto(
            paciente.Id,
            paciente.Nome,
            paciente.Telefone,
            paciente.CartaoSus,
            paciente.Cpf));
    }

    private bool ExistePacienteComMesmoCartao(string cartaoSus, Guid? idIgnorado = null)
    {
        List<Paciente> fornecedors = repositorioPaciente.SelecionarTodos();

        return fornecedors.Any(f => f.Id != idIgnorado && string.Equals(f.CartaoSus, cartaoSus, StringComparison.OrdinalIgnoreCase));
    }

    private static Result ValidarEntidade(Paciente paciente)
    {
        List<string> erros = paciente.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }


    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}

