// Decompiled with JetBrains decompiler
// Type: SRPG.TowerHPColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass == null)
        return;
      this.ChangeValue(MonoSingleton<GameManager>.Instance.TowerResuponse.GetPlayerUnitHP(dataOfClass), (int) dataOfClass.Status.param.hp);
    }

    public void ChangeValue(int hp, int max_hp)
    {
      float num = (float) hp / (float) max_hp;
      if ((double) num >= 1.0)
        this.image.sprite = this.ColorBlue;
      else if ((double) num > 0.200000002980232)
        this.image.sprite = this.ColorGreen;
      else
        this.image.sprite = this.ColorRed;
    }
  }
}
