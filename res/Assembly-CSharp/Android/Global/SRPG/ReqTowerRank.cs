// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerRank : WebAPI
  {
    public ReqTowerRank(string qid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/ranking";
      stringBuilder.Append("\"qid\":\"");
      stringBuilder.Append(qid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class JSON_TowerRankParam
    {
      public string fuid;
      public string name;
      public int lv;
      public int rank;
      public int score;
      public string uid;
      public Json_Unit unit;
    }

    public class JSON_TowerRankResponse
    {
      public ReqTowerRank.JSON_TowerRankParam[] speed;
      public ReqTowerRank.JSON_TowerRankParam[] technical;
    }
  }
}
