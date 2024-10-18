// Decompiled with JetBrains decompiler
// Type: SRPG.JsonEscape
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class JsonEscape
  {
    public static string Escape(string s)
    {
      if (s == null || s.Length == 0)
        return string.Empty;
      int length = s.Length;
      StringBuilder stringBuilder = new StringBuilder(length);
      for (int index = 0; index < length; ++index)
      {
        char ch = s[index];
        switch (ch)
        {
          case '\b':
            stringBuilder.Append("\\b");
            break;
          case '\t':
            stringBuilder.Append("\\t");
            break;
          case '\n':
            stringBuilder.Append("\\n");
            break;
          case '\f':
            stringBuilder.Append("\\f");
            break;
          case '\r':
            stringBuilder.Append("\\r");
            break;
          default:
            if (ch != '"')
            {
              if (ch != '/')
              {
                if (ch != '\\')
                {
                  if (ch <= '\u007F')
                  {
                    stringBuilder.Append(ch);
                    break;
                  }
                  string str = ((int) ch).ToString("X");
                  stringBuilder.Append("\\u" + str.PadLeft(4, '0'));
                  break;
                }
              }
              else
              {
                stringBuilder.Append('\\');
                stringBuilder.Append(ch);
                break;
              }
            }
            stringBuilder.Append('\\');
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }
  }
}
