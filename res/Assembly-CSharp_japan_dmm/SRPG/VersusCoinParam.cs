// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCoinParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusCoinParam
  {
    public string iname;
    public string coin_iname;
    public int win_cnt;
    public int lose_cnt;
    public int draw_cnt;

    public void Deserialize(JSON_VersusCoin json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      this.coin_iname = json.coin_iname;
      this.win_cnt = json.win_cnt;
      this.lose_cnt = json.lose_cnt;
      this.draw_cnt = json.draw_cnt;
    }
  }
}
