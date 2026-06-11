using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionario.Apresentacao;

public class FuncionarioController(ServicoFuncionario servicoFuncionario, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFuncionariosDto> dtos = servicoFuncionario.SelecionarTodos();

        List<ListarFuncionarioViewModel> listarVms = mapeador.Map<List<ListarFuncionarioViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFuncionarioViewModel cadastrarVm = new CadastrarFuncionarioViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarFuncionarioDto dto = mapeador.Map<CadastrarFuncionarioDto>(cadastrarVm);

        Result resultado = servicoFuncionario.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid Id)
    {
        Result<DetalhesFuncionariosDto> resultado = servicoFuncionario.SelecionarPorId(Id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarFuncionarioViewModel editarVm = mapeador.Map<EditarFuncionarioViewModel>(resultado.Value);
        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarFuncionariosDto dto = mapeador.Map<EditarFuncionariosDto>(editarVm);

        Result resultado = servicoFuncionario.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid Id)
    {
        Result<DetalhesFuncionariosDto> resultado = servicoFuncionario.SelecionarPorId(Id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirFuncionarioViewModel excluirVm = mapeador.Map<ExcluirFuncionarioViewModel>(resultado.Value);
        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFuncionarioViewModel excluirVm)
    {
        Result resultado = servicoFuncionario.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

        }

        TempData.AddSuccessMessage(resultado);
        return RedirectToAction(nameof(Listar));
    }
}
