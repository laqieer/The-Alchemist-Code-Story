// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_InfinitySpawnGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
