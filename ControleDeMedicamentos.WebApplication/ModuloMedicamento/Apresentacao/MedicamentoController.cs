using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Apresentacao;

public class MedicamentoController : Controller
{
    ServicoMedicamento servicoMedicamento;
    IMapper mapeador;

    public MedicamentoController(ServicoMedicamento servicoMedicamento, IMapper mapeador)
    {
        this.servicoMedicamento = servicoMedicamento;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMedicamentosDto> dtos = servicoMedicamento.SelecionarTodos();
        List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel cadastrarVm = new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            Guid.Empty,
            SelecionarFornecedores()
        );

        return View(cadastrarVm);
    }


    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });

        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(cadastrarVm);

        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesMedicamentoDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarMedicamentoViewModel editarVm =
            mapeador.Map<EditarMedicamentoViewModel>(resultado.Value) with { Fornecedores = SelecionarFornecedores() };

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Fornecedores = SelecionarFornecedores() });

        EditarMedicamentoDto dto = mapeador.Map<EditarMedicamentoDto>(editarVm);

        Result resultado = servicoMedicamento.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm with { Fornecedores = SelecionarFornecedores() });
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesMedicamentoDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirMedicamentoViewModel excluirVm = mapeador.Map<ExcluirMedicamentoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMedicamentoViewModel excluirVm)
    {
        Result resultado = servicoMedicamento.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoFornecedorViewModel> SelecionarFornecedores()
    {
        List<OpcaoFornecedorDto> dtos = servicoMedicamento.SelecionarFornecedor();

        return mapeador.Map<List<OpcaoFornecedorViewModel>>(dtos);
    }
}
