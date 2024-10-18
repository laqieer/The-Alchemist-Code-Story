// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusRankClassParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_VersusRankClassParam
  {
    public int schedule_id;
    public int type;
    public int up_pt;
    public int down_pt;
    public int down_losing_streak;
    public string reward_id;
    public int win_pt_max;
    public int win_pt_min;
    public int lose_pt_max;
    public int lose_pt_min;
  }
}
