using AutoMapper;
using ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Apresentacao;

public class EstoqueSaidaController : Controller
{
    private readonly ServicoEstoqueSaida servicoEstoqueSaida;
    private readonly IMapper mapeador;

    public EstoqueSaidaController(ServicoEstoqueSaida servicoEstoqueSaida, IMapper mapeador)
    {
        this.servicoEstoqueSaida = servicoEstoqueSaida;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar(Guid pacienteId)
    {
        Result<GerenciarEstoqueSaidaDto> resultado = servicoEstoqueSaida.SelecionarTodosPorPaciente(pacienteId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction("Listar", "Paciente");
        }

        GerenciarEstoqueSaidaViewModel gerenciarVm = mapeador.Map<GerenciarEstoqueSaidaViewModel>(resultado.Value);

        return View(gerenciarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid pacienteId)
    {
        CadastrarEstoqueSaidaViewModel cadastrarVm = new CadastrarEstoqueSaidaViewModel(
            pacienteId,
            Guid.Empty,
            1,
            DateTime.Now,
            SelecionarMedicamentos()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEstoqueSaidaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Medicamentos = SelecionarMedicamentos() });

        CadastrarEstoqueSaidaDto dto = mapeador.Map<CadastrarEstoqueSaidaDto>(cadastrarVm);

        Result resultado = servicoEstoqueSaida.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(cadastrarVm with { Medicamentos = SelecionarMedicamentos() });
        }

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar), new { pacienteId = cadastrarVm.PacienteId });
    }

    private List<OpcaoMedicamentoViewModel> SelecionarMedicamentos()
    {
        List<OpcaoMedicamentoDto> dtos = servicoEstoqueSaida.SelecionarMedicamentos();

        return mapeador.Map<List<OpcaoMedicamentoViewModel>>(dtos);
    }

}
