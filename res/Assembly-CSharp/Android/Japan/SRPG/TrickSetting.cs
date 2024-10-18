// Decompiled with JetBrains decompiler
// Type: SRPG.TrickSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
