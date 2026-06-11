using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionarios.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<ListarFuncionariosDto, ListarFuncionarioViewModel>();
        CreateMap<CadastrarFuncionarioViewModel, CadastrarFuncionarioDto>();
        CreateMap<EditarFuncionarioViewModel, EditarFuncionariosDto>();
        CreateMap<DetalhesFuncionariosDto, EditarFuncionarioViewModel>();
        CreateMap<DetalhesFuncionariosDto, ExcluirFuncionarioViewModel>();
    }
}