// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class HighlightParam
  {
    public string iname;
    public DateTime begin_at;
    public DateTime end_at;
    public DateTime data_end_at;
    public HighlightResource[] resources;
    public HighlightGift gift;

    public void Deserialze(JSON_HighlightParam json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      DateTime.TryParse(json.begin_at, out this.begin_at);
      DateTime.TryParse(json.end_at, out this.end_at);
      DateTime.TryParse(json.data_end_at, out this.data_end_at);
      if (json.resources == null)
        return;
      HighlightResource[] highlightResourceArray = new HighlightResource[json.resources.Length];
      for (int index = 0; index < json.resources.Length; ++index)
      {
        HighlightResource highlightResource = new HighlightResource();
        highlightResource.Deserialize(json.resources[index]);
        highlightResourceArray[index] = highlightResource;
      }
      this.resources = highlightResourceArray;
    }

    public bool IsAvailable() => this.IsAvailable(TimeManager.ServerTime);

    public bool IsAvailable(DateTime time) => time >= this.begin_at && time < this.end_at;
  }
}
