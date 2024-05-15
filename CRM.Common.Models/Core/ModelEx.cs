using System.ComponentModel.DataAnnotations;

namespace CRM.Common.Models.Core;

public class ModelEx
{

  [Key]
  [SqlKata.Ignore]
  [SqlKata.Column("id")]
  public int Id { get; set; }

  [SqlKata.Column("createdby")]
  public string CreatedBy { get; set; } = string.Empty;

  [SqlKata.Column("modifiedby")]
  public string ModifiedBy { get; set; } = string.Empty;

  [SqlKata.Column("createddate")]
  public DateTime CreatedDate { get; set; }

  [SqlKata.Column("modifieddate")]
  public DateTime ModifiedDate { get; set; }

  [SqlKata.Column("isarchived")]
  public bool IsArchived { get; set; }

  [SqlKata.Column("isactive")]
  public bool IsActive { get; set; }
}

