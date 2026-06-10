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

    [Required(ErrorMessage ="O campo \"Telefone\"deve ser preenchido")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"CNPJ\"deve ser preenchido")]
    string CNPJ
);

public record EditarFornecedorViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Nome\"deve ser preenchido")]
    [StringLength(100,MinimumLength =3,ErrorMessage ="O nome deve ter entre 3 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Telefone\"deve ser preenchido")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"CNPJ\"deve ser preenchido")]
    string CNPJ
);

public record ExcluirFornecedorViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);
