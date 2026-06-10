using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Apresentacao;

public record OpcaoFornecedorViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record ListarMedicamentosViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);

public record CadastrarMedicamentoViewModel(
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(100, MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Descrição\" deve ser preenchido")]
    [StringLength(255,MinimumLength =5,ErrorMessage ="A descrição deve ter entre 5 e 255 caracteres")]
    string Descricao,

    [Required(ErrorMessage ="Selecione um fornecedor")]
    Guid FornecedorId,

    [ValidateNever]
    List<OpcaoFornecedorViewModel> Fornecedores
);

public record EditarMedicamentoViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(100, MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Descrição\" deve ser preenchido")]
    [StringLength(255,MinimumLength =5,ErrorMessage ="A descrição deve ter entre 5 e 255 caracteres")]
    string Descricao,

    [Required(ErrorMessage ="Selecione um fornecedor")]
    Guid FornecedorId,

    [ValidateNever]
    List<OpcaoFornecedorViewModel> Fornecedores
);

public record ExcluirMedicamentoViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);
