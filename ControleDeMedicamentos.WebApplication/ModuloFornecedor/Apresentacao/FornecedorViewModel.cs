using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedorViewModel(
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

     [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage = "O campo CNPJ deve ser preenchido")]
    [RegularExpression(
      @"^\d{14}$",
        ErrorMessage = "CNPJ inválido,deve ter 14 digitos")]
    string CNPJ
);

public record EditarFornecedorViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

     [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$",ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage = "O campo CNPJ deve ser preenchido")]
    [RegularExpression(
      @"^\d{14}$",
        ErrorMessage = "CNPJ inválido")]
    string CNPJ
);

public record ExcluirFornecedorViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);
