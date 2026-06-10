using System;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarFornecedorDto dto)
    {
        if (ExisteFornecedorComMesmoCNPJ(dto.CNPJ))
        {
            return Falha("CNPJ", "Já existe um fornecedor com esse CNPJ");
        }

        Fornecedor novoFornecedor = new Fornecedor(
            dto.Nome,
            dto.Telefone,
            dto.CNPJ
        );

        Result resultadoValidacao = ValidarEntidade(novoFornecedor);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        repositorioFornecedor.Cadastrar(novoFornecedor);
        return Result.Ok().WithSuccess("Fornecedor cadastrado com sucesso");
    }

    public Result Editar(EditarFornecedorDto dto)
    {
        if (ExisteFornecedorComMesmoCNPJ(dto.CNPJ, dto.Id))
        {
            return Falha("CNPJ", "Já existe um fornecedor com esse CNPJ");
        }

        Fornecedor fornecedorAtualizado = new Fornecedor(
            dto.Nome,
            dto.Telefone,
            dto.CNPJ
        );

        Result resultadoValidacao = ValidarEntidade(fornecedorAtualizado);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        bool conseguiuEditar = repositorioFornecedor.Editar(dto.Id, fornecedorAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Fornecedor não encontrado.");


        return Result.Ok().WithSuccess("Fornecedor editado com sucesso");
    }

    public Result Excluir(Guid Id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(Id);

        if (fornecedor == null)
        {
            return Result.Fail("Fornecedor não encontrado");
        }

        repositorioFornecedor.Excluir(Id);

        return Result.Ok().WithSuccess("Fornecedor excluido com sucesso");
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        return repositorioFornecedor.SelecionarTodos()
        .Select(f => new ListarFornecedoresDto(
            f.Id,
            f.Nome,
            f.Telefone,
            f.CNPJ)).ToList();
    }

    public Result<DetalhesFornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok(new DetalhesFornecedorDto(
            fornecedor.Id,
            fornecedor.Nome,
            fornecedor.Telefone,
            fornecedor.CNPJ));
    }

    private bool ExisteFornecedorComMesmoCNPJ(string cNPJ, Guid? idIgnorado = null)
    {
        List<Fornecedor> fornecedors = repositorioFornecedor.SelecionarTodos();

        return fornecedors.Any(f => f.Id != idIgnorado && string.Equals(f.CNPJ, cNPJ, StringComparison.OrdinalIgnoreCase));
    }

    private static Result ValidarEntidade(Fornecedor fornecedor)
    {
        List<string> erros = fornecedor.Validar();

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

