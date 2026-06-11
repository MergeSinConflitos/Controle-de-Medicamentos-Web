using System;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;

public class ServicoEstoqueEntrada
{
    private readonly IRepositorioEstoqueEntrada repositorioEstoqueEntrada;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoEstoqueEntrada(IRepositorioEstoqueEntrada repositorioEstoqueEntrada, IRepositorioMedicamento repositorioMedicamento, IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioEstoqueEntrada = repositorioEstoqueEntrada;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    
}
