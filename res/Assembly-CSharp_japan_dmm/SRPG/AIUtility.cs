// Decompiled with JetBrains decompiler
// Type: SRPG.AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public static class AIUtility
  {
    public static bool IsFailCondition(EUnitCondition condition)
    {
      return condition != EUnitCondition.AutoHeal && condition != EUnitCondition.GoodSleep && condition != EUnitCondition.AutoJewel && condition != EUnitCondition.Fast && condition != EUnitCondition.DisableDebuff && condition != EUnitCondition.DisableKnockback;
    }

    public static bool IsFailCondition(Unit self, Unit target, EUnitCondition condition)
    {
      bool flag = SceneBattle.Instance.Battle.CheckEnemySide(self, target);
      return AIUtility.IsFailCondition(condition) ? flag : !flag;
    }
  }
}
