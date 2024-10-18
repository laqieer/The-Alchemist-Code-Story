// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCoinParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
