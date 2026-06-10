using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Apresentacao;

public class PacienteController : Controller
{
    IMapper mapeador;
    ServicoPaciente servicoPaciente;

    public PacienteController(IMapper mapeador, ServicoPaciente servicoPaciente)
    {
        this.mapeador = mapeador;
        this.servicoPaciente = servicoPaciente;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacientesDto> dtos = servicoPaciente.SelecionarTodos();

        List<ListarPacientesViewModel> listarVms = mapeador.Map<List<ListarPacientesViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel cadastrarVm = new CadastrarPacienteViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarPacienteDto dto = mapeador.Map<CadastrarPacienteDto>(cadastrarVm);

        Result resultado = servicoPaciente.Cadastrar(dto);

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
        Result<DetalhesPacienteDto> resultado = servicoPaciente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesPacienteDto dto = resultado.Value;

        EditarPacienteViewModel editarVm = mapeador.Map<EditarPacienteViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarPacienteDto dto = mapeador.Map<EditarPacienteDto>(editarVm);

        Result resultado = servicoPaciente.Editar(dto);

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
        Result<DetalhesPacienteDto> resultado = servicoPaciente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesPacienteDto dto = resultado.Value;

        ExcluirPacienteViewModel excluirVm = mapeador.Map<ExcluirPacienteViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        Result resultado = servicoPaciente.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
        {
            TempData.AddSuccessMessage(resultado);
        }
        return RedirectToAction(nameof(Listar));
    }
}

