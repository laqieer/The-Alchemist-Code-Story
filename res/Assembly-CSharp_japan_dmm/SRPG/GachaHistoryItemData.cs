// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GachaHistoryItemData
  {
    private GachaHistoryData[] mHistorys;
    private string mGachaTitle;
    private long mDropAt;

    public GachaHistoryItemData(GachaHistoryData[] historys, string title, long drop_at)
    {
      this.mHistorys = historys;
      this.mGachaTitle = title;
      this.mDropAt = drop_at;
    }

    public GachaHistoryData[] historys => this.mHistorys;

    public string gachaTitle => this.mGachaTitle;

    public long drop_at => this.mDropAt;

    public DateTime GetDropAt() => GameUtility.UnixtimeToLocalTime(this.mDropAt);
  }
}
