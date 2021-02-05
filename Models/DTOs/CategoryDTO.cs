using System.Collections.Generic;

public class CategoryDTO
{
  public int Id { get; set; }
  public string CategoryName { get; set; }
  public string Description { get; set; }
  public string ImgUrl { get; set; }

  ICollection<ProductCategoryDTO> ProductCategoriesDTO { get; set; }
}