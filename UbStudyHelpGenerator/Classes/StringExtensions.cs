using System;
using System.Diagnostics;
using System.Text;

public static class StringExtensions
{
    public static string ToDebugString(this StringBuilder sb, int maxLength = 20)
    {
        if (sb == null) return null;
        if (sb.Length <= maxLength) return sb.ToString();

        return "..." + sb.ToString(sb.Length - maxLength, maxLength);
    }
}
