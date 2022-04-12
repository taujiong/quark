using System.Text.RegularExpressions;

namespace System;

public static class StringExtensions
{
  public static string ToCamelCase(this string str)
  {
    if (string.IsNullOrWhiteSpace(str))
    {
      return str;
    }

    if (str.Length == 1)
    {
      return str.ToLowerInvariant();
    }

    return char.ToLowerInvariant(str[0]) + str[1..];
  }

  public static string ToKebabCase(this string str)
  {
    if (string.IsNullOrWhiteSpace(str))
    {
      return str;
    }

    str = str.ToCamelCase();

    return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
  }
}
