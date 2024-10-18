// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusRankClassParam
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
