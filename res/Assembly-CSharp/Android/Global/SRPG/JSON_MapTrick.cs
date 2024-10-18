// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
