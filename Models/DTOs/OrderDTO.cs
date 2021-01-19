using System.Collections.Generic;

public class OrderDTO
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public string PaymentMethod { get; set; }
  // public DateTime Created { get; set; }

  public Customer Customer { get; set; }

  ICollection<ProductOrder> ProductOrders { get; set; }
}