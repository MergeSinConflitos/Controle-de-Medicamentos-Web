using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Apresentacao;

public record ListarEstoqueEntradaViewModel(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    int QuantidadeEmEstoque,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record CadastrarEstoqueEntradaViewModel(
    Guid MedicamentoId,  

    [Required(ErrorMessage = "A seleção de \"Funcionário\" é obrigatória.")]
    Guid FuncionarioId,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor positivo (maior que 0).")]
    int Quantidade,

    bool ConfirmarEntrada,

    [ValidateNever]
    List<OpcaoFuncionarioViewModel> MedicamentosDisponiveis
);

public record OpcaoFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record DetalhesEstoqueEntradaViewModel(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    int QuantidadeEmEstoque,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record GerenciarEstoqueEntradaViewModel(
    Guid MedicamentoId,
    string MedicamentoNome,
    List<ListarEstoqueEntradaViewModel> Entradas
);
