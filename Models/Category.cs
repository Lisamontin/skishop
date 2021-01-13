using System.Collections.Generic;

public class Category
{
  public int Id { get; set; }
  public string CategoryName { get; set; }
  public string Description { get; set; }
  public string ImgUrl { get; set; }

  ICollection<ProductCategory> ProductCategories { get; set; }//is naming ok? "productcategories"
}