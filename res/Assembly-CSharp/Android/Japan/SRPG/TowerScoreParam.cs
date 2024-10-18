// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScoreParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
