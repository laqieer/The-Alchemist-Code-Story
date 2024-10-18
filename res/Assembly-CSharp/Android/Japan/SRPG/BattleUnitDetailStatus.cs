// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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

    public void SetStatus(BattleUnitDetailStatus.eBudStat stat, int val, int add)
    {
      int num = (int) stat;
      if (num >= 0)
      {
        ImageArray component = this.GetComponent<ImageArray>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && num < component.Images.Length)
          component.ImageIndex = num;
      }
      if ((bool) ((UnityEngine.Object) this.StatusValue))
        this.StatusValue.gameObject.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.StatusValueUp))
        this.StatusValueUp.gameObject.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.StatusValueDown))
        this.StatusValueDown.gameObject.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.GoUpBG))
        this.GoUpBG.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.GoDownBG))
        this.GoDownBG.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.UpDownValue))
        this.UpDownValue.gameObject.SetActive(false);
      if (add > 0)
      {
        if ((bool) ((UnityEngine.Object) this.StatusValueUp))
        {
          this.StatusValueUp.text = val.ToString();
          this.StatusValueUp.gameObject.SetActive(true);
        }
        if ((bool) ((UnityEngine.Object) this.UpDownValue))
        {
          this.UpDownValue.text = "+" + add.ToString();
          this.UpDownValue.gameObject.SetActive(true);
        }
        if (!(bool) ((UnityEngine.Object) this.GoUpBG))
          return;
        this.GoUpBG.SetActive(true);
      }
      else if (add < 0)
      {
        if ((bool) ((UnityEngine.Object) this.StatusValueDown))
        {
          this.StatusValueDown.text = val.ToString();
          this.StatusValueDown.gameObject.SetActive(true);
        }
        if ((bool) ((UnityEngine.Object) this.UpDownValue))
        {
          this.UpDownValue.text = add.ToString();
          this.UpDownValue.gameObject.SetActive(true);
        }
        if (!(bool) ((UnityEngine.Object) this.GoDownBG))
          return;
        this.GoDownBG.SetActive(true);
      }
      else
      {
        if (!(bool) ((UnityEngine.Object) this.StatusValue))
          return;
        this.StatusValue.text = val.ToString();
        this.StatusValue.gameObject.SetActive(true);
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
