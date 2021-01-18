using AutoMapper;

public class AutoMap : Profile
{
  public AutoMap()
  {
    CreateMap<Product, ProductDTO>();
    CreateMap<ProductDTO, Product>();

    CreateMap<ProductOrder, ProductOrderDTO>();
    CreateMap<ProductOrderDTO, ProductOrder>();
  }
}