using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {

        CreateMap<OpcaoFornecedorDto, OpcaoFornecedorViewModel>();
        CreateMap<ListarMedicamentosDto, ListarMedicamentosViewModel>();
        CreateMap<CadastrarMedicamentoViewModel, CadastrarMedicamentoDto>();
        CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>();

        CreateMap<DetalhesMedicamentoDto, EditarMedicamentoViewModel>()
            .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcaoFornecedorViewModel>()));

        CreateMap<DetalhesMedicamentoDto, ExcluirMedicamentoViewModel>();
    }
}
