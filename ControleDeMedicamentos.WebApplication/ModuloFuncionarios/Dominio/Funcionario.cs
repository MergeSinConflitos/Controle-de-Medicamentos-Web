using System;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Dominio;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void Atualizar(Funcionario entidadeAtualizada)
    {
        Funcionario FuncionarioAtualizada = (Funcionario)entidadeAtualizada;

        Nome = FuncionarioAtualizada.Nome;
        Telefone = FuncionarioAtualizada.Telefone;
        Cpf = FuncionarioAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo '/Nome/' é obrigatório");
        
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome deve ter entre 3 e 100 caracteres");
        
        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo 'Telefone' é obrigatório");
        else if (Telefone.Length != 13 && Telefone.Length != 14)
            erros.Add("O telefone deve estar no formato (XX)XXXX-XXXX ou (XX)XXXXX-XXXX");
        
        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo 'CPF' é obrigatório");
        else if (Cpf.Length != 11)
            erros.Add("O CPF deve ter 11 dígitos");

        return erros;
    }

}
