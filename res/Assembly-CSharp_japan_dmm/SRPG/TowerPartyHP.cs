// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyHP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TowerPartyHP : MonoBehaviour, IGameParameter
  {
    public Slider mSlider;

    private void Start() => this.Refresh();

    public void UpdateValue() => this.Refresh();

    public void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
        return;
      UnitData UnitData = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (UnitData == null)
      {
        ((Component) this.mSlider).gameObject.SetActive(false);
      }
      else
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        if (towerResuponse == null)
          ((Component) this.mSlider).gameObject.SetActive(false);
        else if (towerResuponse.pdeck == null)
        {
          ((Component) this.mSlider).gameObject.SetActive(true);
          int hp = (int) UnitData.Status.param.hp;
          this.SetSliderValue(hp, hp);
        }
        else
        {
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => x.unitname == UnitData.UnitParam.iname));
          if (playerUnit == null)
          {
            ((Component) this.mSlider).gameObject.SetActive(true);
            int hp = (int) UnitData.Status.param.hp;
            this.SetSliderValue(hp, hp);
          }
          else if (playerUnit.isDied)
          {
            ((Component) this.mSlider).gameObject.SetActive(false);
          }
          else
          {
            ((Component) this.mSlider).gameObject.SetActive(true);
            this.SetSliderValue(Mathf.Max((int) UnitData.Status.param.hp - playerUnit.dmg, 1), (int) UnitData.Status.param.hp);
          }
        }
      }
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
        return;
      this.mSlider.maxValue = (float) maxValue;
      this.mSlider.minValue = 0.0f;
      this.mSlider.value = (float) value;
    }
  }
}
