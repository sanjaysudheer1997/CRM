using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
[Table("employee")]
public class Employee : Model
{
  [SqlKata.Column("firstname")]
  public string Firstname { get; set; }

  [SqlKata.Column("lastname")]
  public string Lastname { get; set; }

  [SqlKata.Column("email")]
  public string Email { get; set; }

  [SqlKata.Column("phone")]
  public string Phone { get; set; }

  [SqlKata.Column("address")]
  public string Address { get; set; }

  [SqlKata.Column("city")]
  public string City { get; set; }

  [SqlKata.Column("state")]
  public string State { get; set; }

  [SqlKata.Column("postalcode")]
  public string Postalcode { get; set; }

  [SqlKata.Column("country")]
  public string Country { get; set; }

  [SqlKata.Column("dateofbirth")]
  public DateTime Dateofbirth { get; set; }

  [SqlKata.Column("hiredate")]
  public DateTime Hiredate { get; set; }

  [SqlKata.Column("department")]
  public string Department { get; set; }

  [SqlKata.Column("position")]
  public string Position { get; set; }

  [SqlKata.Column("userid")]
  public string Userid { get; set; }

  [SqlKata.Column("serviceproviderid")]
  public int Serviceproviderid { get; set; }

  [SqlKata.Column("currentavailabilitystatus")]
  public int Currentavailabilitystatus { get; set; }

  [SqlKata.Column("username")]
  [Required(ErrorMessage = "Username is required.")]
  [RegularExpression(@"^[A-Za-z@$!%*?&]+$",
    ErrorMessage = "Username cannot contain digits and should only consist of letters and symbols.")]
  public string Username { get; set; }

  [SqlKata.Column("password")]
  [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
  ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
  [Required(ErrorMessage = "Password is required.")]
  public string Password { get; set; }


  [SqlKata.Column("logo")]
  public byte[] Logo { get; set; }

  [SqlKata.Column("verificationstatus")]
  public int Verificationstatus { get; set; }

  [SqlKata.Column("level")]
  public int Level { get; set; }

}