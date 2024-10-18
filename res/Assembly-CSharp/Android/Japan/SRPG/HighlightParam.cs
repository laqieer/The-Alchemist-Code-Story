// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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

    public bool IsAvailable()
    {
      return this.IsAvailable(TimeManager.ServerTime);
    }

    public bool IsAvailable(DateTime time)
    {
      if (time >= this.begin_at)
        return time < this.end_at;
      return false;
    }
  }
}
