// Decompiled with JetBrains decompiler
// Type: SRPG.EquipRecipeItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EquipRecipeItem : MonoBehaviour
  {
    public Color DefaultLineColor;
    public Color CommonEquipLineColor;
    public Color DefaultTextColor;
    public Color CommonEquipTextColor;
    public Image[] Lines;
    public Text EquipItemNum;
    public GameObject CommonText;
    public GameObject CommonIcon;

    private void Start()
    {
    }

    public void SetIsCommon(bool is_common)
    {
      if ((UnityEngine.Object) this.EquipItemNum == (UnityEngine.Object) null)
        return;
      this.EquipItemNum.color = !is_common ? this.DefaultTextColor : this.CommonEquipTextColor;
      if ((UnityEngine.Object) this.CommonText != (UnityEngine.Object) null)
        this.CommonText.SetActive(is_common);
      if (!((UnityEngine.Object) this.CommonIcon != (UnityEngine.Object) null))
        return;
      this.CommonIcon.SetActive(is_common);
    }

    public void SetIsCommonLine(bool is_common)
    {
      if (this.Lines == null)
        return;
      for (int index = 0; index < this.Lines.Length; ++index)
        this.Lines[index].color = !is_common ? this.DefaultLineColor : this.CommonEquipLineColor;
    }
  }
}
