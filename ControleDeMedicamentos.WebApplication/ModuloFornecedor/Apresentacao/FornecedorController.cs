using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Apresentacao;

public class FornecedorController : Controller
{
    IMapper mapeador;
    ServicoFornecedor servicoFornecedor;

    public FornecedorController(IMapper mapeador, ServicoFornecedor servicoFornecedor)
    {
        this.mapeador = mapeador;
        this.servicoFornecedor = servicoFornecedor;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFornecedoresDto> dtos = servicoFornecedor.SelecionarTodos();

        List<ListarFornecedoresViewModel> listarVms = mapeador.Map<List<ListarFornecedoresViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFornecedorViewModel cadastrarVm = new CadastrarFornecedorViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarFornecedorDto dto = mapeador.Map<CadastrarFornecedorDto>(cadastrarVm);

        Result resultado = servicoFornecedor.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesFornecedorDto> resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesFornecedorDto dto = resultado.Value;

        EditarFornecedorViewModel editarVm = mapeador.Map<EditarFornecedorViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFornecedorViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarFornecedorDto dto = mapeador.Map<EditarFornecedorDto>(editarVm);

        Result resultado = servicoFornecedor.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesFornecedorDto> resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesFornecedorDto dto = resultado.Value;

        ExcluirFornecedorViewModel excluirVm = mapeador.Map<ExcluirFornecedorViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFornecedorViewModel excluirVm)
    {
        Result resultado = servicoFornecedor.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
        {
            TempData.AddSuccessMessage(resultado);
        }
        return RedirectToAction(nameof(Listar));
    }
}
