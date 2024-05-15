namespace Core.Models;

public class Model
{
  public int Id { get; set; }
  public string CreatedBy { get; set; }
  public string ModifiedBy { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime ModifiedDate { get; set; }
  public bool IsArchived { get; set; }
  public bool IsActive { get; set; }
}
