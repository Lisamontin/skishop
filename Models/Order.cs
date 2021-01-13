using System;

public class Order
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public string PaymentMethod { get; set; }
  public DateTime Created { get; set; }
}