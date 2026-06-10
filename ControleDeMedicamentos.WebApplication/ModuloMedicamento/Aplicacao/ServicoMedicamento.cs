using System;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoMedicamento(IRepositorioMedicamento repositorioMedicamento, IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedor == null)
        {
            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor válido");
        }

        Medicamento novoMedicamento = new Medicamento(
            dto.Nome,
            dto.Descricao,
            fornecedor
        );

        Result resultadoValidacao = ValidarEntidade(novoMedicamento);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        repositorioMedicamento.Cadastrar(novoMedicamento);
        return Result.Ok().WithSuccess("Medicamento cadastrado com sucesso");
    }

    public Result Editar(EditarMedicamentoDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedor == null)
        {
            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor válido");
        }

        Medicamento medicamentoAtualizado = new Medicamento(
            dto.Nome,
            dto.Descricao,
            fornecedor
        );

        Result resultadoValidacao = ValidarEntidade(medicamentoAtualizado);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        bool conseguiuEditar = repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        if (!conseguiuEditar)
        {
            return Result.Fail("Medicamento não encontrado");
        }

        return Result.Ok().WithSuccess("Medicamento editado com sucesso");
    }

    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
        {
            return Result.Fail("Medicamento não encontrado");
        }

        repositorioMedicamento.Excluir(id);

        return Result.Ok().WithSuccess("Medicamento excluido com sucesso");
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        return repositorioMedicamento.SelecionarTodos()
        .Select(m => new ListarMedicamentosDto(
            m.Id,
            m.Nome,
            m.Descricao,
            m.QuantidadeEmEstoque,
            m.Fornecedor.Id,
            m.Fornecedor.Nome
        )).ToList();
    }

    public Result<DetalhesMedicamentoDto> SelecionarPorId(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
        {
            return Result.Fail("Medicamento não encontrado");
        }

        return Result.Ok(new DetalhesMedicamentoDto(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.QuantidadeEmEstoque,
            medicamento.Fornecedor.Id,
            medicamento.Fornecedor.Nome
        ));
    }

    public List<OpcaoFornecedorDto> SelecionarFornecedor()
    {
        return repositorioFornecedor.SelecionarTodos()
        .Select(f => new OpcaoFornecedorDto(
            f.Id,
            f.Nome,
            f.Telefone,
            f.CNPJ)).ToList();
    }

    private static Result ValidarEntidade(Medicamento medicamento)
    {
        List<string> erros = medicamento.Validar();

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
