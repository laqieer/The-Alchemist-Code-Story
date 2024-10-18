// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListTotalCombatPower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitListTotalCombatPower : FlowWindowBase
  {
    private UnitListWindow m_Root;
    private GameObject m_TotalCombatPowerRoot;
    private Text m_TotalCombatPowerTitle;
    private Text m_TotalCombatPowerText;

    public override string name => nameof (UnitListTotalCombatPower);

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      base.Initialize(param);
      if (param is UnitListTotalCombatPower.SerializeParam serializeParam)
      {
        this.m_TotalCombatPowerTitle = serializeParam.totalCombatPowerTitle;
        this.m_TotalCombatPowerText = serializeParam.totalCombatPowerText;
        this.m_TotalCombatPowerRoot = serializeParam.totalCombatPowerRoot;
      }
      this.Close(true);
    }

    public void SetRoot(UnitListWindow root) => this.m_Root = root;

    private void SetTotalCombatPowerTitle(UnitListRootWindow.Tab tabType)
    {
      switch (tabType)
      {
        case UnitListRootWindow.Tab.FAVORITE:
          this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_FAVORITE");
          break;
        case UnitListRootWindow.Tab.FIRE:
          this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_FIRE");
          break;
        case UnitListRootWindow.Tab.WATER:
          this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_WATER");
          break;
        case UnitListRootWindow.Tab.THUNDER:
          this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_THUNDER");
          break;
        default:
          if (tabType != UnitListRootWindow.Tab.WIND)
          {
            if (tabType != UnitListRootWindow.Tab.LIGHT)
            {
              if (tabType != UnitListRootWindow.Tab.DARK)
              {
                if (tabType != UnitListRootWindow.Tab.ALL)
                  break;
                this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_POWER");
                break;
              }
              this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_DARK");
              break;
            }
            this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_SHINE");
            break;
          }
          this.m_TotalCombatPowerTitle.text = LocalizedText.Get("sys.COMBAT_POWER_RANKING_TOTAL_WIND");
          break;
      }
    }

    private void SetTotalCombatPower(long totalCombatPower)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TotalCombatPowerText, (UnityEngine.Object) null))
        return;
      this.m_TotalCombatPowerText.text = totalCombatPower.ToString();
    }

    public override int OnActivate(int pinId)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Root, (UnityEngine.Object) null) && this.m_Root.rootWindow != null)
      {
        UnitListRootWindow.ContentType contentType = this.m_Root.rootWindow.GetContentType();
        GameUtility.SetGameObjectActive(this.m_TotalCombatPowerRoot, contentType == UnitListRootWindow.ContentType.UNIT);
        if (contentType == UnitListRootWindow.ContentType.UNIT && (pinId == 410 || pinId == 430 || pinId == 100 || pinId == 101 || pinId == 102 || pinId == 103 || pinId == 105 || pinId == 104 || pinId == 310 || pinId == 400 || pinId == 490))
        {
          this.SetTotalCombatPowerTitle(this.m_Root.rootWindow.GetCurrentTab());
          this.SetTotalCombatPower(this.CalcCurrentUnitTotalCombatPower());
        }
      }
      return -1;
    }

    private long CalcCurrentUnitTotalCombatPower()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Root, (UnityEngine.Object) null) || this.m_Root.rootWindow == null)
        return 0;
      long total_combat_power = 0;
      this.m_Root.rootWindow.GetListData("unitlist")?.calcData.ForEach((Action<UnitListWindow.Data>) (listData =>
      {
        if (listData.unit == null || listData.unit.IsRental)
          return;
        total_combat_power += (long) listData.unit.CalcTotalParameter();
      }));
      return total_combat_power;
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject totalCombatPowerRoot;
      public Text totalCombatPowerTitle;
      public Text totalCombatPowerText;

      public override System.Type type => typeof (UnitListTotalCombatPower);
    }
  }
}
