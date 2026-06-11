using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloFuncionario.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloFuncionario.Apresentacao;

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