// Decompiled with JetBrains decompiler
// Type: SRPG.EquipRecipeItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Equality((Object) this.EquipItemNum, (Object) null))
        return;
      ((Graphic) this.EquipItemNum).color = !is_common ? this.DefaultTextColor : this.CommonEquipTextColor;
      if (Object.op_Inequality((Object) this.CommonText, (Object) null))
        this.CommonText.SetActive(is_common);
      if (!Object.op_Inequality((Object) this.CommonIcon, (Object) null))
        return;
      this.CommonIcon.SetActive(is_common);
    }

    public void SetIsCommonLine(bool is_common)
    {
      if (this.Lines == null)
        return;
      for (int index = 0; index < this.Lines.Length; ++index)
        ((Graphic) this.Lines[index]).color = !is_common ? this.DefaultLineColor : this.CommonEquipLineColor;
    }
  }
}
