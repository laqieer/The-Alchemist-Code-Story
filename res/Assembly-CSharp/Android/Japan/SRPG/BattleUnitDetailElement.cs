// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class BattleUnitDetailElement : MonoBehaviour
  {
    public ImageArray ImageElement;
    public ImageArray ImageFluct;

    public void SetElement(EElement elem, BattleUnitDetail.eBudFluct fluct)
    {
      if ((bool) ((UnityEngine.Object) this.ImageElement))
      {
        int num = (int) elem;
        if (num >= 0 && num < this.ImageElement.Images.Length)
          this.ImageElement.ImageIndex = num;
      }
      if (!(bool) ((UnityEngine.Object) this.ImageFluct))
        return;
      this.ImageFluct.gameObject.SetActive(fluct != BattleUnitDetail.eBudFluct.NONE);
      if (fluct == BattleUnitDetail.eBudFluct.NONE)
        return;
      int num1 = (int) fluct;
      if (num1 >= this.ImageFluct.Images.Length)
        return;
      this.ImageFluct.ImageIndex = num1;
    }
  }
}
