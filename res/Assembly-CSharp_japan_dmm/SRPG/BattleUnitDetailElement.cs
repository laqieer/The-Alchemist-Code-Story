// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetailElement : MonoBehaviour
  {
    public ImageArray ImageElement;
    public ImageArray ImageFluct;

    public void SetElement(EElement elem, BattleUnitDetail.eBudFluct fluct)
    {
      if (Object.op_Implicit((Object) this.ImageElement))
      {
        int num = (int) elem;
        if (num >= 0 && num < this.ImageElement.Images.Length)
          this.ImageElement.ImageIndex = num;
      }
      if (!Object.op_Implicit((Object) this.ImageFluct))
        return;
      ((Component) this.ImageFluct).gameObject.SetActive(fluct != BattleUnitDetail.eBudFluct.NONE);
      if (fluct == BattleUnitDetail.eBudFluct.NONE)
        return;
      int num1 = (int) fluct;
      if (num1 >= this.ImageFluct.Images.Length)
        return;
      this.ImageFluct.ImageIndex = num1;
    }
  }
}
