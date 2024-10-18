// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyHP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerPartyHP : MonoBehaviour, IGameParameter
  {
    public Slider mSlider;

    private void Start()
    {
      this.Refresh();
    }

    public void UpdateValue()
    {
      this.Refresh();
    }

    public void Refresh()
    {
      if ((UnityEngine.Object) this.mSlider == (UnityEngine.Object) null)
        return;
      UnitData UnitData = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (UnitData == null)
      {
        this.mSlider.gameObject.SetActive(false);
      }
      else
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        if (towerResuponse == null)
          this.mSlider.gameObject.SetActive(false);
        else if (towerResuponse.pdeck == null)
        {
          this.mSlider.gameObject.SetActive(true);
          int hp = (int) UnitData.Status.param.hp;
          this.SetSliderValue(hp, hp);
        }
        else
        {
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => x.unitname == UnitData.UnitParam.iname));
          if (playerUnit == null)
          {
            this.mSlider.gameObject.SetActive(true);
            int hp = (int) UnitData.Status.param.hp;
            this.SetSliderValue(hp, hp);
          }
          else if (playerUnit.isDied)
          {
            this.mSlider.gameObject.SetActive(false);
          }
          else
          {
            this.mSlider.gameObject.SetActive(true);
            this.SetSliderValue(Mathf.Max((int) UnitData.Status.param.hp - playerUnit.dmg, 1), (int) UnitData.Status.param.hp);
          }
        }
      }
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!((UnityEngine.Object) this.mSlider != (UnityEngine.Object) null))
        return;
      this.mSlider.maxValue = (float) maxValue;
      this.mSlider.minValue = 0.0f;
      this.mSlider.value = (float) value;
    }
  }
}
