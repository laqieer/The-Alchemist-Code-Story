// Decompiled with JetBrains decompiler
// Type: SRPG.TrickSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
