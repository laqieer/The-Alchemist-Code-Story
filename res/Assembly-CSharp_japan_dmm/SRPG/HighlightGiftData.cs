// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightGiftData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class HighlightGiftData
  {
    public HighlightGiftType type;
    public string item;
    public int num;

    public void Deserialize(JSON_HighlightGiftData json)
    {
      if (json == null)
        return;
      this.type = (HighlightGiftType) json.type;
      this.item = json.item;
      this.num = json.num;
    }
  }
}
