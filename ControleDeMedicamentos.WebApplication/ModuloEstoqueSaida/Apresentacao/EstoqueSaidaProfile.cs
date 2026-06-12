using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloEstoqueSaida.Apresentacao;

public class EstoqueSaidaProfile : Profile
{
    public EstoqueSaidaProfile()
    {
        CreateMap<ListarEstoqueSaidaDto, ListarEstoqueSaidaViewModel>();
        CreateMap<GerenciarEstoqueSaidaDto, GerenciarEstoqueSaidaViewModel>();
        CreateMap<OpcaoMedicamentoDto, OpcaoMedicamentoViewModel>();
        CreateMap<CadastrarEstoqueSaidaViewModel, CadastrarEstoqueSaidaDto>();
    }
}
