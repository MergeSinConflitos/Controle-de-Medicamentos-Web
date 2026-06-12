namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Aplicacao;

public record CadastrarEstoqueSaidaDto(
    Guid PacienteId,
    Guid MedicamentoId,
    int Quantidade,
    DateTime Data
);

public record ListarEstoqueSaidaDto(
    Guid Id,
    DateTime Data,
    Guid PacienteId,
    string NomePaciente,
    Guid MedicamentoId,
    string NomeMedicamento,
    int QuantidadeEmEstoque,
    int Quantidade
);

public record DetalhesEstoqueSaidaDto(
    Guid Id,
    DateTime Data,
    Guid PacienteId,
    string NomePaciente,
    Guid MedicamentoId,
    string NomeMedicamento,
    int QuantidadeEmEstoque,
    int Quantidade
);

public record OpcaoMedicamentoDto(
    Guid Id,
    string Nome,
    int QuantidadeEmEstoque
);