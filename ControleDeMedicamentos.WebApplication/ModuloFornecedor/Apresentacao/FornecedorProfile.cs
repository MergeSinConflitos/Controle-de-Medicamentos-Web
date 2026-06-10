using System;
using AutoMapper;
using ControleDeMedicamentos.WebApplication.ModuloFornecedor.Aplicacao;

namespace ControleDeMedicamentos.WebApplication.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<ListarFornecedoresDto, ListarFornecedoresViewModel>();
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedorDto>();
        CreateMap<EditarFornecedorViewModel, EditarFornecedorDto>();
        CreateMap<DetalhesFornecedorDto, EditarFornecedorViewModel>();
        CreateMap<DetalhesFornecedorDto, ExcluirFornecedorViewModel>();
    }
}
