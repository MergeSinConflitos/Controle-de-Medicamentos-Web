using System;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Aplicacao;

public record OpcaoFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);

public record CadastrarMedicamentoDto(
    string Nome,
    string Descricao,
    Guid FornecedorId
);

public record EditarMedicamentoDto(
    Guid Id,
    string Nome,
    string Descricao,
    Guid FornecedorId

);

public record DetalhesMedicamentoDto(
    Guid Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);