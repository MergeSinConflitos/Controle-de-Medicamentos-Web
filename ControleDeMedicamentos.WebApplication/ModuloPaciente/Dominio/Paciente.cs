using System;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApplication.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSus { get; set; }
    public string Cpf { get; set; }

    public Paciente()
    {
    }

    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }
    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Paciente listaAtualizada = (Paciente)entidadeAtualizada;

        Nome = listaAtualizada.Nome;
        Telefone = listaAtualizada.Telefone;
        CartaoSus = listaAtualizada.CartaoSus;
        Cpf = listaAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo '/Nome/' é obrigatório");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome deve ter entre 3 e 100 caracteres");



        Regex regex = new(@"^\(?[1-9]{2}\)?\s?9\d{4}-?\d{4}$"); //cria um formato valido para Telefone

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo '/Telefone/' é obrigatório");
        }
        else if (!regex.IsMatch(Telefone))
        {
            erros.Add("O telefone deve estar em um formato valido (11 digitos)");
        }

        Regex regexCartaoSus = new(@"^\d{15}$"); //cria um formato valido para o Cartao do sus

        if (string.IsNullOrWhiteSpace(CartaoSus))
        {
            erros.Add("O campo '/Cartão do Sus/' é obrigatório");
        }
        else if (!regexCartaoSus.IsMatch(CartaoSus))
        {
            erros.Add("O cartão deve ter 15 digitos");
        }

        Regex regexCpf = new(@"^\d{11}$"); //cria um formato valido para o Cpf

        if (string.IsNullOrWhiteSpace(Cpf))
        {
            erros.Add("O campo '/Cpf/' é obrigatório");
        }
        else if (!regexCpf.IsMatch(Cpf))
        {
            erros.Add("O Cpf deve ter 11 digitos");
        }


        return erros;
    }
}


