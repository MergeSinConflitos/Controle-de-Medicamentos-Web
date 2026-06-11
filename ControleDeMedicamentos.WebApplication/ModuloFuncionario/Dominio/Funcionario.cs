using System;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;

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
        
        Regex regex = new(@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$");          //cria um formato valido para Telefone

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo '/Telefone/' é obrigatório");
        }
        else if (!regex.IsMatch(Telefone))
        {
            erros.Add("O telefone deve estar em um formato valido (11 digitos)");
        }
        
        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo 'CPF' é obrigatório");
        else if (Cpf.Length != 11)
            erros.Add("O CPF deve ter 11 dígitos");

        return erros;
    }

}
