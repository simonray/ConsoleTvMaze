using System;
using System.Text;

public static class Extensions
{    
    public static string RemovePunctuation(this string @self)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in self)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }
        return sb.ToString();
    }
}
