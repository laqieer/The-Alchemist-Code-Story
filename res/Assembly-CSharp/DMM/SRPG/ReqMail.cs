// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqMail : WebAPI
  {
    public ReqMail(int page, bool isPeriod, bool isRead, Network.ResponseCallback response)
    {
      this.name = "mail";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append(this.MakeKeyValue(nameof (page), page));
      stringBuilder.Append(",");
      stringBuilder.Append(this.MakeKeyValue(nameof (isPeriod), !isPeriod ? 0 : 1));
      stringBuilder.Append(",");
      stringBuilder.Append(this.MakeKeyValue(nameof (isRead), !isRead ? 0 : 1));
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    private string MakeKeyValue(string key, int value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    private string MakeKeyValue(string key, float value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    private string MakeKeyValue(string key, long value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    private string MakeKeyValue(string key, string value)
    {
      return string.Format("\"{0}\":\"{1}\"", (object) key, (object) value);
    }
  }
}
