// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

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
