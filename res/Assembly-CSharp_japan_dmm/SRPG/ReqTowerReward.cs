// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
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
