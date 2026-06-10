using System;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CNPJ { get; set; }

    public Fornecedor(string nome, string telefone, string cNPJ)
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cNPJ;
    }

    public Fornecedor()
    {
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Fornecedor fornecedorAtualizado = (Fornecedor)entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        CNPJ = fornecedorAtualizado.CNPJ;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add("O campo '/Nome/' é obrigatório");
        }
        else if (Nome.Length < 3 || Nome.Length > 100)
        {
            erros.Add("O nome deve ter entre 3 e 100 caracteres");
        }


        Regex regex = new(@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$"); //cria um formato valido para Telefone

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo '/Telefone/' é obrigatório");
        }
        else if (!regex.IsMatch(Telefone))
        {
            erros.Add("O telefone deve estar em um formato valido (11 digitos)");
        }

        Regex regexCnpj = new(@"^\d{14}$"); //cria um formato valido para o CNPJ

        if (string.IsNullOrWhiteSpace(CNPJ))
        {
            erros.Add("O campo '/CNPJ/' é obrigatório");
        }
        else if (!regexCnpj.IsMatch(CNPJ))
        {
            erros.Add("O CNPJ deve ter 14 digitos");
        }


        return erros;
    }
}
