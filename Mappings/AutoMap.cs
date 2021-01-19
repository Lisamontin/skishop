using AutoMapper;

public class AutoMap : Profile
{
  public AutoMap()
  {
    CreateMap<Product, ProductDTO>();
    CreateMap<ProductDTO, Product>();

    CreateMap<ProductOrder, ProductOrderDTO>();
    CreateMap<ProductOrderDTO, ProductOrder>();

    CreateMap<ProductCategory, ProductCategoryDTO>();
    CreateMap<ProductCategoryDTO, ProductCategory>();

    CreateMap<Category,CategoryDTO>();
    CreateMap<CategoryDTO, Category>();

    CreateMap<Order, OrderDTO>();
    CreateMap<OrderDTO, Order>();

    CreateMap<Customer, CustomerDTO>();
    CreateMap<CustomerDTO, Customer>();
  }
}