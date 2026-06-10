using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<ListarPacientesDto, ListarPacientesViewModel>();
        CreateMap<CadastrarPacienteViewModel, CadastrarPacienteDto>();
        CreateMap<EditarPacienteViewModel, EditarPacienteDto>();
        CreateMap<DetalhesPacienteDto, EditarPacienteViewModel>();
        CreateMap<DetalhesPacienteDto, ExcluirPacienteViewModel>();
    }
}
