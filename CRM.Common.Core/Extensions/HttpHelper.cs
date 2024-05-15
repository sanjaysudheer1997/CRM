namespace CRM.Common.Core.Extensions;


public static class HttpHelper
{
  private static HttpContext _context;

  public static HttpContext Context
  {
    get
    {
      return _context;
    }
  }

  public static void Configure(HttpContext context)
  {
    _context = context;
  }

  public static string IdentityUserName
  {
    get
    {
      if (_context.User != null)
      {
        return _context.User.Identity.Name;
      }

      return null;
    }
  }

  public static List<string> GetRoles()
  {
    if (_context.User != null && _context.User.Claims != null)
    {
      return _context.User.Claims
      .Where(c => c.Type == ClaimTypes.Role)
      .Select(c => c.Value)
      .ToList();
    }

    return null;
  }

  public static List<string> GetModules()
  {
    if (_context.User != null && _context.User.Claims != null)
    {
      return _context.User.Claims
      .Where(c => c.Type == "Module")
      .Select(c => c.Value)
      .ToList();
    }

    return null;
  }

  public static string Username
  {
    get
    {
      try
      {
        var userClaims = _context.User;
        var username = userClaims.FindFirst(ClaimTypes.Name)?
            .Value
               ?? userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? "system";

        return username;
      }
      catch
      {
        return "system";
      }
    }
  }

  public static string FirstName
  {
    get
    {
      try
      {
        var userClaims = _context.User;
        var firstName = userClaims.FindFirst(ClaimTypes.GivenName)?
            .Value
               ?? userClaims.FindFirst(ClaimTypes.GivenName)?.Value
               ?? "system";

        return firstName;
      }
      catch
      {
        return "system";
      }
    }
  }
}


