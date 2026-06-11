using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Apresentacao;

public record ListarFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record CadastrarFuncionarioViewModel(
    
    [Required(ErrorMessage ="O campo \"Nome\" é obrigatório")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [RegularExpression(
      @"^\d{11}$",
        ErrorMessage = "CPF inválido,deve ter 11 digitos")]
    string Cpf
);

public record EditarFuncionarioViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Nome\" é obrigatório")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [RegularExpression(
      @"^\d{11}$",
        ErrorMessage = "CPF inválido,deve ter 11 digitos")]
    string Cpf
);

public record ExcluirFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);            