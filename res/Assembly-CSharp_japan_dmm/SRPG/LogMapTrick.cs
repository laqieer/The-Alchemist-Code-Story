// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class LogMapTrick : BattleLog
  {
    public TrickData TrickData;
    public List<LogMapTrick.TargetInfo> TargetInfoLists = new List<LogMapTrick.TargetInfo>();

    public class TargetInfo
    {
      public Unit Target;
      public bool IsEffective;
      public int Heal;
      public int Damage;
      public EUnitCondition FailCondition;
      public EUnitCondition CureCondition;
      public Grid KnockBackGrid;
    }
  }
}
