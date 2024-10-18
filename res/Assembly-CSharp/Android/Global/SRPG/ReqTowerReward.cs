// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerReward : WebAPI
  {
    public ReqTowerReward(short mid, short nid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "expedition/reward";
      stringBuilder.Append("\"mid\":");
      stringBuilder.Append(mid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"nid\":");
      stringBuilder.Append(nid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
