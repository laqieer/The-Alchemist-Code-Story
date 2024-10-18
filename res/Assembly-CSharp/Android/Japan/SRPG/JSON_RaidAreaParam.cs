// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidAreaParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_RaidAreaParam : JSON_RaidMasterParam
  {
    public int id;
    public int order;
    public int period_id;
    public int boss_count;
    public int area_boss_id;
    public int clear_reward_id;
  }
}
