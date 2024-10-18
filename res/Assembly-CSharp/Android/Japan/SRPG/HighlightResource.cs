// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class HighlightResource
  {
    public HighlightType type;
    public string path;
    public string message;

    public void Deserialize(JSON_HighlightResource json)
    {
      if (json == null)
        return;
      this.type = (HighlightType) json.type;
      this.path = json.path;
      this.message = json.message;
    }
  }
}
