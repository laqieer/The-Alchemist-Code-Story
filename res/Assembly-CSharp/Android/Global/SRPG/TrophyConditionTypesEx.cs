// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyConditionTypesEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public static class TrophyConditionTypesEx
  {
    public static bool IsExtraClear(this TrophyConditionTypes type)
    {
      switch (type)
      {
        case TrophyConditionTypes.exclear_fire:
        case TrophyConditionTypes.exclear_water:
        case TrophyConditionTypes.exclear_wind:
        case TrophyConditionTypes.exclear_thunder:
        case TrophyConditionTypes.exclear_light:
        case TrophyConditionTypes.exclear_dark:
        case TrophyConditionTypes.exclear_fire_nocon:
        case TrophyConditionTypes.exclear_water_nocon:
        case TrophyConditionTypes.exclear_wind_nocon:
        case TrophyConditionTypes.exclear_thunder_nocon:
        case TrophyConditionTypes.exclear_light_nocon:
        case TrophyConditionTypes.exclear_dark_nocon:
          return true;
        default:
          return false;
      }
    }
  }
}
