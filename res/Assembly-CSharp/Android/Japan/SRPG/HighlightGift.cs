// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
