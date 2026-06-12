using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Apresentacao;

public class EstoqueEntradaController : Controller
{
    ServicoEstoqueEntrada servicoEstoqueEntrada;
    IMapper mapeador;

    public EstoqueEntradaController(ServicoEstoqueEntrada servicoEstoqueEntrada, IMapper mapeador)
    {
        this.servicoEstoqueEntrada = servicoEstoqueEntrada;
        this.mapeador = mapeador;
    }

    public ActionResult Listar(Guid medicamentoId)
    {
        List<ListarEstoqueEntradaDto> dtos =
            servicoEstoqueEntrada.SelecionarTodosPorMedicamento(medicamentoId);

        GerenciarEstoqueEntradaViewModel gerenciarVm =
            new GerenciarEstoqueEntradaViewModel(
                medicamentoId,
                dtos.FirstOrDefault()?.MedicamentoNome ?? string.Empty, mapeador.Map<List<ListarEstoqueEntradaViewModel>>(dtos));

        return View(gerenciarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid medicamentoId)
    {
        CadastrarEstoqueEntradaViewModel cadastrarVm = new CadastrarEstoqueEntradaViewModel(
                medicamentoId,
                Guid.Empty,
                1,
                false,
                SelecionarFuncionarios()
            );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEstoqueEntradaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with
            {
                Funcionarios = SelecionarFuncionarios()
            });

        CadastrarEstoqueEntradaDto dto =
            mapeador.Map<CadastrarEstoqueEntradaDto>(cadastrarVm);

        Result resultado = servicoEstoqueEntrada.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with
            {
                Funcionarios = SelecionarFuncionarios()
            });
        }

        if (cadastrarVm.AdicionarOutro)
            return RedirectToAction(nameof(Cadastrar),new { medicamentoId = cadastrarVm.MedicamentoId });

        return RedirectToAction(nameof(Listar),
            new { medicamentoId = cadastrarVm.MedicamentoId });
    }

    private List<OpcaoFuncionarioViewModel> SelecionarFuncionarios()
    {
        List<OpcaoFuncionarioDto> dtos =
            servicoEstoqueEntrada.SelecionarFuncionarios();

        return mapeador.Map<List<OpcaoFuncionarioViewModel>>(dtos);
    }
}
