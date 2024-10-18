// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemAbilPointPaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqItemAbilPointPaid : WebAPI
  {
    public ReqItemAbilPointPaid(Network.ResponseCallback response)
    {
      this.name = "item/addappaid";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }

    public ReqItemAbilPointPaid(int value, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "item/addappaid";
      stringBuilder.Append("\"val\" : ");
      stringBuilder.Append(value);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
