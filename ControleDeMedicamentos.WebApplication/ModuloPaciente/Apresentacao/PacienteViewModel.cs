using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Apresentacao;

public record ListarPacientesViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preechido")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Telefone\"deve ser preenchido")]
    [RegularExpression(
@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$",ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"Cartão do Sus\"deve ser preenchido")]
    [RegularExpression(   @"^\d{15}$", ErrorMessage ="O cartão deve conter 15 digitos")]
    string CartaoSus,

    [Required(ErrorMessage ="O campo \"Cpf\"deve ser preenchido")]
    [RegularExpression(   @"^\d{11}$", ErrorMessage ="Cpf inválido")]
    string Cpf
);

public record EditarPacienteViewModel(
    Guid Id,
    [Required(ErrorMessage ="O campo \"Nome\"deve ser preechido")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Telefone\"deve ser preenchido")]
    [RegularExpression(
@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$",ErrorMessage = "Telefone inválido")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"Cartão do Sus\"deve ser preenchido")]
    [RegularExpression(   @"^\d{15}$", ErrorMessage ="O cartão deve conter 15 digitos")]
    string CartaoSus,

    [Required(ErrorMessage ="O campo \"Cpf\"deve ser preenchido")]
    [RegularExpression(   @"^\d{11}$", ErrorMessage ="Cpf inválido")]
    string Cpf
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

