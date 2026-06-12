using System;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;

public class ServicoEstoqueEntrada
{
    private readonly IRepositorioEstoqueEntrada repositorioEstoqueEntrada;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoEstoqueEntrada(IRepositorioEstoqueEntrada repositorioEstoqueEntrada, IRepositorioMedicamento repositorioMedicamento, IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioEstoqueEntrada = repositorioEstoqueEntrada;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public Result Cadastrar(CadastrarEstoqueEntradaDto dto)
    {
        Result<(Funcionario Funcionario, Medicamento Medicamento)> resultadoRelacionamentos =
        SelecionarRelacionamentos(dto.FuncionarioId, dto.MedicamentoId);

        if (resultadoRelacionamentos.IsFailed)
            return Result.Fail(resultadoRelacionamentos.Errors);
        
        EstoqueEntrada novaEntrada = new EstoqueEntrada(
            resultadoRelacionamentos.Value.Medicamento,
            resultadoRelacionamentos.Value.Funcionario,
            dto.Quantidade);

        Result resultadoValidacao = ValidarEntidade(novaEntrada);
        
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        resultadoRelacionamentos.Value.Medicamento.RegistrarEntrada(dto.Quantidade);

        repositorioEstoqueEntrada.Cadastrar(novaEntrada);

        repositorioMedicamento.Editar(
           dto.MedicamentoId, resultadoRelacionamentos.Value.Medicamento);

        return Result.Ok().WithSuccess("Requisição feita com sucesso");
    }

    public List<ListarEstoqueEntradaDto> SelecionarTodosPorMedicamento(Guid medicamentoId)
    {
        return repositorioEstoqueEntrada
            .Filtrar(e => e.Medicamento.Id == medicamentoId)
            .Select(MapearParaListarDto)
            .ToList();
    }

    public Result<DetalhesEstoqueEntradaDto> SelecionarPorId(Guid id)
    {
        EstoqueEntrada? entrada = repositorioEstoqueEntrada.SelecionarPorId(id);

        if (entrada == null)
            return Result.Fail("Requisição de entrada não encontrada.");

        return Result.Ok(MapearParaDetalhesDto(entrada));
    }

    public List<OpcaoFuncionarioDto> SelecionarFuncionarios()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new OpcaoFuncionarioDto(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cpf
            ))
            .ToList();
    }

    private Result<(Funcionario Funcionario, Medicamento Medicamento)> SelecionarRelacionamentos(
       Guid FuncionarioId,
       Guid MedicamentoId
   )
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(FuncionarioId);

        if (funcionario == null)
            return Result.Fail(new Error("Selecione um funcionário valido.").WithMetadata("Campo", nameof(FuncionarioId)));

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(MedicamentoId);

        if (medicamento == null)
            return Result.Fail(new Error("Selecione um medicamento válido.").WithMetadata("Campo", nameof(MedicamentoId)));

        return Result.Ok((funcionario, medicamento));
    }
    private static Result ValidarEntidade(EstoqueEntrada estoqueEntrada)
    {
        List<string> erros = estoqueEntrada.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

     private static ListarEstoqueEntradaDto MapearParaListarDto(EstoqueEntrada estoque)
    {
        return new ListarEstoqueEntradaDto(
            estoque.Id,
            estoque.Data,
            estoque.Medicamento.Id,
            estoque.Medicamento.Nome,
            estoque.Medicamento.QuantidadeEmEstoque,
            estoque.Funcionario.Id,
            estoque.Funcionario.Nome,
            estoque.Quantidade
        );
    }
    private static DetalhesEstoqueEntradaDto MapearParaDetalhesDto(EstoqueEntrada estoque)
    {
        return new DetalhesEstoqueEntradaDto(
            estoque.Id,
            estoque.Data,
            estoque.Medicamento.Id,
            estoque.Medicamento.Nome,
            estoque.Medicamento.QuantidadeEmEstoque,
            estoque.Funcionario.Id,
            estoque.Funcionario.Nome,
            estoque.Quantidade
        );
    }

}
