// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuest_OldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuest_OldData
  {
    public int player_lv;
    public int player_exp;
    public UnitData[] units;

    public void Init(Json_Unit[] json_units, int p_lv, int p_exp)
    {
      this.player_lv = p_lv;
      this.player_exp = p_exp;
      List<UnitData> unitDataList = new List<UnitData>();
      if (json_units != null)
      {
        for (int index = 0; index < json_units.Length; ++index)
        {
          if (json_units[index] != null)
          {
            UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(json_units[index].iid);
            if (unitDataByUniqueId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUniqueId);
              unitDataList.Add(unitData);
            }
          }
        }
      }
      this.units = unitDataList.ToArray();
    }
  }
}
