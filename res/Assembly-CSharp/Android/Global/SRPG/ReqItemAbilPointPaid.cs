// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemAbilPointPaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

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
