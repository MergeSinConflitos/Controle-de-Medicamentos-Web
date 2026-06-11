namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;

public record ListarEstoqueEntradaDto(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    int QuantidadeEmEstoque,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record CadastrarEstoqueEntradaDto(
    Guid MedicamentoId,
    Guid FuncionarioId,
    int Quantidade
);

public record DetalhesEstoqueEntradaDto(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    int QuantidadeEmEstoque,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record OpcaoEntradaDto(
    Guid Id,
    string Nome,
    string Descricao,
    string QuantidadeEmEstoque
);