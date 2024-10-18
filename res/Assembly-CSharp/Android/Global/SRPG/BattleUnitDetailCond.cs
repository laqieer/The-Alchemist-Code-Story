// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailCond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitDetailCond : MonoBehaviour
  {
    private string[] mStrShieldDesc = new string[4]{ string.Empty, "quest.BUD_COND_SHIELD_DETAIL_COUNT", "quest.BUD_COND_SHIELD_DETAIL_HP", "quest.BUD_COND_SHIELD_DETAIL_LIMIT" };
    public ImageArray ImageCond;
    public Text TextValue;

    public void SetCond(EUnitCondition cond)
    {
      int index = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).IndexOf(cond);
      if ((bool) ((UnityEngine.Object) this.ImageCond) && index >= 0 && index < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = index;
      if (!(bool) ((UnityEngine.Object) this.TextValue) || index < 0 || index >= Unit.StrNameUnitConds.Length)
        return;
      this.TextValue.text = Unit.StrNameUnitConds[index];
    }

    public void SetCondShield(ShieldTypes s_type, int val)
    {
      int count = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).Count;
      if ((bool) ((UnityEngine.Object) this.ImageCond) && count >= 0 && count < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = count;
      if (!(bool) ((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = string.Format(LocalizedText.Get("quest.BUD_COND_SHIELD_DETAIL"), (object) string.Format(LocalizedText.Get(this.mStrShieldDesc[(int) s_type]), (object) val));
    }
  }
}
