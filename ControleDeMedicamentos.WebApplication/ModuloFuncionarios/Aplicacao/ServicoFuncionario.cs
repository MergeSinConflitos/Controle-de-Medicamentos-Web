using ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Aplicacao;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if(ExisteFuncionarioComCpf(dto.Cpf))
        {
            return Falha(nameof(dto.Cpf), "Já existe um Funcionario com esse CPF");
        }

        Funcionario novoFuncionario = new Funcionario(dto.Nome,dto.Telefone,dto.Cpf);

        Result resultadoValidacao = ValidarEntidade(novoFuncionario);

        if(resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Cadastrar(novoFuncionario);

        return Result.Ok().WithSuccess("Funcionario cadastrado com sucesso");
    }

    public Result Editar(EditarFuncionariosDto dto)
    {
        if(ExisteFuncionarioComCpf(dto.Cpf))
        {
            return Falha(nameof(dto.Cpf), "Já existe um Funcionario com esse CPF");
        }

        Funcionario funcionarioAtualizado = new Funcionario(dto.Nome,dto.Telefone,dto.Cpf);

        Result resultdadoValidacao = ValidarEntidade(funcionarioAtualizado);

        if(resultdadoValidacao.IsFailed)
            return resultdadoValidacao;

        bool conseguiEditar = repositorioFuncionario.Editar(dto.Id, funcionarioAtualizado);

        if(!conseguiEditar)
            return Result.Fail("Funcionario não encontrado.");

        return Result.Ok().WithSuccess("Funcionario editado com sucesso");
    }

    public Result Excluir(Guid Id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(Id);

        if (funcionario == null)
            return Result.Fail("Funcionario não encontrado.");
        
        repositorioFuncionario.Excluir(Id);
         
        return Result.Ok().WithSuccess("Funcionario excluido com sucesso");
    }

    public List<ListarFuncionariosDto> SelecionarTodos()
    {
        return repositorioFuncionario.SelecionarTodos()
        .Select(f => new ListarFuncionariosDto(
            f.Id,
            f.Nome,
            f.Telefone,
            f.Cpf)).ToList();
    }

    public Result<DetalhesFuncionariosDto> SelecionarPorId(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionario não encontrado.");

        return Result.Ok(new DetalhesFuncionariosDto(funcionario.Id, funcionario.Nome, funcionario.Telefone,funcionario.Cpf));
    }

    private static Result ValidarEntidade(Funcionario funcionario)
    {
        List<string> erros = funcionario.Validar();

        if(erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private bool ExisteFuncionarioComCpf(string cpf, Guid? idIgnore = null)
    {
        return repositorioFuncionario.SelecionarTodos().Any(f => f.Id != idIgnore
        && string.Equals(f.Cpf, cpf, StringComparison.OrdinalIgnoreCase));
    }

    private Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
