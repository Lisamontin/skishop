using System.Collections.Generic;

public class Customer 
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public int PhoneNumber { get; set; }
  public string Email { get; set; }
  public string Address { get; set; }
  
  ICollection<Order> Orders { get; set; } 
}