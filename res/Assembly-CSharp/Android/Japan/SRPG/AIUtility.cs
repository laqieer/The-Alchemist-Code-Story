// Decompiled with JetBrains decompiler
// Type: SRPG.AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
