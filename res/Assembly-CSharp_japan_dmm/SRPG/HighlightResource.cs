// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
