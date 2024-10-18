// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
