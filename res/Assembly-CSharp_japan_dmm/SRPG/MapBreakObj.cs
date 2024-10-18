// Decompiled with JetBrains decompiler
// Type: SRPG.MapBreakObj
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class MapBreakObj
  {
    public int clash_type;
    public int ai_type;
    public int side_type;
    public int ray_type;
    public int is_ui = 1;
    public int max_hp;
    public int[] rest_hps;

    public void CopyTo(MapBreakObj dst)
    {
      dst.clash_type = this.clash_type;
      dst.ai_type = this.ai_type;
      dst.side_type = this.side_type;
      dst.ray_type = this.ray_type;
      dst.is_ui = this.is_ui;
      dst.max_hp = this.max_hp;
      if (this.rest_hps == null || this.rest_hps.Length == 0)
        return;
      dst.rest_hps = new int[this.rest_hps.Length];
      for (int index = 0; index < this.rest_hps.Length; ++index)
        dst.rest_hps[index] = this.rest_hps[index];
    }
  }
}
