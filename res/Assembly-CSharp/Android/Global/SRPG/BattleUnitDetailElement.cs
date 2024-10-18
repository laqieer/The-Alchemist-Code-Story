// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
