using System.Collections.Generic;

public class ProductDTO
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public string Description { get; set; }
  public int Price { get; set; }
  public string ImgUrl { get; set; }
  public string Brand { get; set; }
  public string Color { get; set; }

  public ICollection<ProductOrderDTO> ProductOrders { get; set; }
  public ICollection<ProductCategoryDTO> ProductCategories { get; set; }
}