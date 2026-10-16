using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductCreateDto, Product>();
    }
}