using System;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;

public record ListarPacientesDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record CadastrarPacienteDto(
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record EditarPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record DetalhesPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);
