// Decompiled with JetBrains decompiler
// Type: SRPG.TowerHPColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TowerHPColor : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite ColorBlue;
    [SerializeField]
    private Sprite ColorGreen;
    [SerializeField]
    private Sprite ColorRed;
    private const float BorderGreen = 1f;
    private const float BorderRed = 0.2f;

    public void UpdateValue()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (dataOfClass == null)
        return;
      this.ChangeValue(MonoSingleton<GameManager>.Instance.TowerResuponse.GetPlayerUnitHP(dataOfClass), (int) dataOfClass.Status.param.hp);
    }

    public void ChangeValue(int hp, int max_hp)
    {
      float num = (float) hp / (float) max_hp;
      if ((double) num >= 1.0)
        this.image.sprite = this.ColorBlue;
      else if ((double) num > 0.20000000298023224)
        this.image.sprite = this.ColorGreen;
      else
        this.image.sprite = this.ColorRed;
    }
  }
}
