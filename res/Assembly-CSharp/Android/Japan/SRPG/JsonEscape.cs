// Decompiled with JetBrains decompiler
// Type: SRPG.JsonEscape
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

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
                  if (ch <= '\x007F')
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
