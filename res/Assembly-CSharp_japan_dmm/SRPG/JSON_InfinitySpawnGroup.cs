// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_InfinitySpawnGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_InfinitySpawnGroup
  {
    public int tag;
    public int deck;
    public int interval;
    public int spawn_max;
    public int spawn_unit_num;
    public int is_spawn_at_start;
    public int is_skip_empty_at_start;
  }
}
