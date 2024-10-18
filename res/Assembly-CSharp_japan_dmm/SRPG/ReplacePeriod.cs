// Decompiled with JetBrains decompiler
// Type: SRPG.ReplacePeriod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ReplacePeriod
  {
    public string mSpriteName;
    public string mBeginAt;
    public string mEndAt;

    public void Deserialize(JSON_ReplacePeriod json)
    {
      this.mSpriteName = json.sprite_name;
      this.mBeginAt = json.begin_at;
      this.mEndAt = json.end_at;
    }
  }
}
