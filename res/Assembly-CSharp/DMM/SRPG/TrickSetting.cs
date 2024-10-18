// Decompiled with JetBrains decompiler
// Type: SRPG.TrickSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TrickSetting
  {
    public string mId;
    public OInt mGx;
    public OInt mGy;
    public string mTag;

    public TrickSetting()
    {
    }

    public TrickSetting(JSON_MapTrick json)
    {
      this.mId = json.id;
      this.mGx = (OInt) json.gx;
      this.mGy = (OInt) json.gy;
      this.mTag = json.tag;
    }
  }
}
