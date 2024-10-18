// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class HighlightGift
  {
    public string iname;
    public string message;
    public HighlightGiftData[] gifts;

    public void Deserialize(JSON_HighlightGift json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      this.message = json.message;
      HighlightGiftData[] highlightGiftDataArray = new HighlightGiftData[json.gifts.Length];
      for (int index = 0; index < json.gifts.Length; ++index)
      {
        HighlightGiftData highlightGiftData = new HighlightGiftData();
        highlightGiftData.Deserialize(json.gifts[index]);
        highlightGiftDataArray[index] = highlightGiftData;
      }
      this.gifts = highlightGiftDataArray;
    }
  }
}
