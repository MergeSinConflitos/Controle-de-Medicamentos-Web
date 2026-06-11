namespace ControleDeMedicamentos.WebApplication.ModuloFuncionario.Aplicacao;

public record ListarFuncionariosDto(
Guid Id, 
string Nome, 
string Telefone, 
string Cpf);

public record CadastrarFuncionarioDto(
string Nome, 
string Telefone,
string Cpf
);

public record EditarFuncionariosDto(
Guid Id, 
string Nome, 
string Telefone, 
string Cpf);

public record DetalhesFuncionariosDto(
Guid Id, 
string Nome, 
string Telefone, 
string Cpf);