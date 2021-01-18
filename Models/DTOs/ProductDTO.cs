using System.Collections.Generic;

public class ProductDTO
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public string Description { get; set; }
  public int Price { get; set; }
  public string ImgUrl { get; set; }
  public int Brand { get; set; }
  public string Color { get; set; }

  ICollection<ProductOrderDTO> ProductOrders { get; set; }
  ICollection<ProductCategoryDTO> ProductCategories { get; set; }
}