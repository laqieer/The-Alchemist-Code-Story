// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetailStatus : MonoBehaviour
  {
    public Text StatusValue;
    public Text StatusValueUp;
    public Text StatusValueDown;
    public GameObject GoUpBG;
    public GameObject GoDownBG;
    public Text UpDownValue;

    public void SetStatus(BattleUnitDetailStatus.eBudStat stat, long val, long add)
    {
      int num = (int) stat;
      if (num >= 0)
      {
        ImageArray component = ((Component) this).GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component, (Object) null) && num < component.Images.Length)
          component.ImageIndex = num;
      }
      if (Object.op_Implicit((Object) this.StatusValue))
        ((Component) this.StatusValue).gameObject.SetActive(false);
      if (Object.op_Implicit((Object) this.StatusValueUp))
        ((Component) this.StatusValueUp).gameObject.SetActive(false);
      if (Object.op_Implicit((Object) this.StatusValueDown))
        ((Component) this.StatusValueDown).gameObject.SetActive(false);
      if (Object.op_Implicit((Object) this.GoUpBG))
        this.GoUpBG.SetActive(false);
      if (Object.op_Implicit((Object) this.GoDownBG))
        this.GoDownBG.SetActive(false);
      if (Object.op_Implicit((Object) this.UpDownValue))
        ((Component) this.UpDownValue).gameObject.SetActive(false);
      if (add > 0L)
      {
        if (Object.op_Implicit((Object) this.StatusValueUp))
        {
          this.StatusValueUp.text = val.ToString();
          ((Component) this.StatusValueUp).gameObject.SetActive(true);
        }
        if (Object.op_Implicit((Object) this.UpDownValue))
        {
          this.UpDownValue.text = "+" + add.ToString();
          ((Component) this.UpDownValue).gameObject.SetActive(true);
        }
        if (!Object.op_Implicit((Object) this.GoUpBG))
          return;
        this.GoUpBG.SetActive(true);
      }
      else if (add < 0L)
      {
        if (Object.op_Implicit((Object) this.StatusValueDown))
        {
          this.StatusValueDown.text = val.ToString();
          ((Component) this.StatusValueDown).gameObject.SetActive(true);
        }
        if (Object.op_Implicit((Object) this.UpDownValue))
        {
          this.UpDownValue.text = add.ToString();
          ((Component) this.UpDownValue).gameObject.SetActive(true);
        }
        if (!Object.op_Implicit((Object) this.GoDownBG))
          return;
        this.GoDownBG.SetActive(true);
      }
      else
      {
        if (!Object.op_Implicit((Object) this.StatusValue))
          return;
        this.StatusValue.text = val.ToString();
        ((Component) this.StatusValue).gameObject.SetActive(true);
      }
    }

    public enum eBudStat
    {
      MHP,
      MMP,
      ATK,
      DEF,
      MAG,
      MND,
      DEX,
      SPD,
      CRI,
      LUK,
      CMB,
      MOV,
      JMP,
      MAX,
    }
  }
}
