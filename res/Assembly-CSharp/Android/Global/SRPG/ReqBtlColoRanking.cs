// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlColoRanking : WebAPI
  {
    public ReqBtlColoRanking(ReqBtlColoRanking.RankingTypes type, Network.ResponseCallback response)
    {
      this.name = "btl/colo/ranking/" + type.ToString();
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }

    public enum RankingTypes
    {
      world,
      friend,
    }
  }
}
