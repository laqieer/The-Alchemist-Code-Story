// Decompiled with JetBrains decompiler
// Type: SRPG.AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public static class AIUtility
  {
    public static bool IsFailCondition(EUnitCondition condition)
    {
      return condition != EUnitCondition.AutoHeal && condition != EUnitCondition.GoodSleep && (condition != EUnitCondition.AutoJewel && condition != EUnitCondition.Fast) && (condition != EUnitCondition.DisableDebuff && condition != EUnitCondition.DisableKnockback);
    }

    public static bool IsFailCondition(Unit self, Unit target, EUnitCondition condition)
    {
      bool flag = SceneBattle.Instance.Battle.CheckEnemySide(self, target);
      if (AIUtility.IsFailCondition(condition))
        return flag;
      return !flag;
    }
  }
}
