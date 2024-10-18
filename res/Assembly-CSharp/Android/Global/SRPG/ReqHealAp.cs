// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqHealAp : WebAPI
  {
    public ReqHealAp(long iid, int num, Network.ResponseCallback response)
    {
      this.name = "item/addstm";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\" : ");
      stringBuilder.Append(iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"num\" : ");
      stringBuilder.Append(num);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
