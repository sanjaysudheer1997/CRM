using CRM.Common.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Common.Models;

[Table("customer")]
public class Customer : ModelEx
{
  [SqlKata.Column("username")]
  public string UserName { get; set; }

  [SqlKata.Column("name")]
  public string Name { get; set; }

  [SqlKata.Column("email")]
  public string Email { get; set; }

  [SqlKata.Column("phone")]
  public string Phone { get; set; }

  [SqlKata.Column("address")]
  [MinLength(10, ErrorMessage = "Address should be at least 10 characters.")]
  [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
  public string Address { get; set; }

  [SqlKata.Column("city")]
  public string City { get; set; }

  [SqlKata.Column("state")]
  public string State { get; set; }

  [SqlKata.Column("postalcode")]
  public string Postalcode { get; set; }

  [SqlKata.Column("country")]
  public string Country { get; set; }

  [SqlKata.Column("contactperson")]
  public string Contactperson { get; set; }

  [SqlKata.Column("industry")]
  public string Industry { get; set; }

  [SqlKata.Column("website")]
  public string Website { get; set; }

  [SqlKata.Column("registrationdate")]
  public DateTime Registrationdate { get; set; }

  [SqlKata.Column("notes")]
  public string Notes { get; set; }

  [SqlKata.Column("customertype")]
  public int CustomerType { get; set; }

  [SqlKata.Column("postoffice")]
  public string PostOffice { get; set; }

  [SqlKata.Column("alternativemobilenumber")]
  public string AlternativeMobileNumber { get; set; }

  [SqlKata.Column("administratorname")]
  public string AdministratorName { get; set; }

  [SqlKata.Column("parentserviceproviderid")]
  public int? Parentserviceproviderid { get; set; }

  [SqlKata.Column("referredbycode")]
  public string ReferredByCode { get; set; }

  [SqlKata.Column("profilepicture")]
  public byte[] ProfilePicture { get; set; }

  [SqlKata.Column("deactivationreasonid")]
  public int? DeactivationReasonId { get; set; }

  [SqlKata.Column("imagepath")]
  public string ImagePath { get; set; }

}
