using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueEntrada.Apresentacao;

public class EstoqueEntradaProfile : Profile
{
    public EstoqueEntradaProfile()
    {
        CreateMap<OpcaoFuncionarioDto, OpcaoFuncionarioViewModel>();
        CreateMap<ListarEstoqueEntradaDto, ListarEstoqueEntradaViewModel>();
        CreateMap<DetalhesEstoqueEntradaDto, DetalhesEstoqueEntradaViewModel>();
        CreateMap<CadastrarEstoqueEntradaViewModel, CadastrarEstoqueEntradaDto>();

    }
}
