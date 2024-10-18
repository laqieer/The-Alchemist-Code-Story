// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScoreParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TowerScoreParam
  {
    public string Rank;
    public OInt Score;
    public OInt TurnCnt;
    public OInt DiedCnt;
    public OInt RetireCnt;
    public OInt RecoverCnt;

    public bool Deserialize(JSON_TowerScoreThreshold json)
    {
      if (json == null)
        return false;
      this.Rank = json.rank;
      this.Score = (OInt) json.score;
      this.TurnCnt = (OInt) json.turn;
      this.DiedCnt = (OInt) json.died;
      this.RetireCnt = (OInt) json.retire;
      this.RecoverCnt = (OInt) json.recover;
      return true;
    }
  }
}
