using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Apresentacao;

public record ListarEstoqueSaidaViewModel(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    int Quantidade
);

public record OpcaoMedicamentoViewModel(
    Guid Id,
    string Nome,
    int QuantidadeEmEstoque
);

public record CadastrarEstoqueSaidaViewModel(
   Guid PacienteId,

    [Required(ErrorMessage = "Selecione um medicamento.")]
    Guid MedicamentoId,

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade,

    [Required(ErrorMessage = "O campo \"Data\" deve ser preenchido.")]
    DateTime Data,

    [ValidateNever]
    List<OpcaoMedicamentoViewModel> Medicamentos
);

public record GerenciarEstoqueSaidaViewModel(
    Guid PacienteId,
    string PacienteNome,
    List<ListarEstoqueSaidaViewModel> Saidas
);