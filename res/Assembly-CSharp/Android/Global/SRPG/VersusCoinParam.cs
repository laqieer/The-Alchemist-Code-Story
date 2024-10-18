// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCoinParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
