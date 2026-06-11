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
        Result<DetalhesFuncionarioDto> resultado = servicoFuncionario.SelecionarPorId(Id);
    }
}
