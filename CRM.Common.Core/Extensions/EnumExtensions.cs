namespace CRM.Common.Core.Extensions;

public static class EnumExtensions
{
  public static string GetDisplayName(this Enum value)
  {
    FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

    if (fieldInfo == null)
      return value.ToString();

    var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

    return displayAttribute?.Name ?? value.ToString();
  }

  public static string GetDisplayAttributeName(Enum value)
  {
    var displayAttribute = value.GetType().GetMember(value.ToString())[0]
        .GetCustomAttributes(typeof(DisplayAttribute), false)
        .Cast<DisplayAttribute>()
        .FirstOrDefault();

    return displayAttribute?.Name ?? value.ToString();
  }
}

[AttributeUsage(AttributeTargets.Field)]
public class EnumIdAttribute : Attribute
{
  public int Id { get; }

  public EnumIdAttribute(int id)
  {
    Id = id;
  }
}
public class EnumMember
{
  public string Name { get; set; }
  public int Value { get; set; }
}
