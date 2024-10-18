// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_MapTrick
  {
    public string id;
    public int gx;
    public int gy;
    public string tag;

    public void CopyTo(JSON_MapTrick dst)
    {
      dst.id = this.id;
      dst.gx = this.gx;
      dst.gy = this.gy;
      dst.tag = this.tag;
    }
  }
}
