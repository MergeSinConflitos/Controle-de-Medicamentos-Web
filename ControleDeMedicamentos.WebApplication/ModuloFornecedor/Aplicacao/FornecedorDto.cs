using System;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;

public record ListarFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedorDto(
    string Nome,
    string Telefone,
    string CNPJ
);

public record EditarFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record DetalhesFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);
