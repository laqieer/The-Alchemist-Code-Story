// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class LogMapTrick : BattleLog
  {
    public List<LogMapTrick.TargetInfo> TargetInfoLists = new List<LogMapTrick.TargetInfo>();
    public TrickData TrickData;

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
