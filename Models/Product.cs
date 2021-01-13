using System.Collections.Generic;

public class Product
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public string Description { get; set; }
  public int Price { get; set; }
  public string ImgUrl { get; set; }
  public int Brand { get; set; }
  public string Color { get; set; }

  ICollection<ProductOrder> ProductOrders { get; set; }
  ICollection<ProductCategory> ProductCategories { get; set; }
}