// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailCond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetailCond : MonoBehaviour
  {
    public ImageArray ImageCond;
    public Text TextValue;
    private string[] mStrShieldDesc = new string[4]
    {
      string.Empty,
      "quest.BUD_COND_SHIELD_DETAIL_COUNT",
      "quest.BUD_COND_SHIELD_DETAIL_HP",
      "quest.BUD_COND_SHIELD_DETAIL_LIMIT"
    };

    public void SetCond(EUnitCondition cond)
    {
      int index = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).IndexOf(cond);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null && index >= 0 && index < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = index;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue) || index < 0 || index >= Unit.StrNameUnitConds.Length)
        return;
      this.TextValue.text = Unit.StrNameUnitConds[index];
    }

    public void SetCondShield(ShieldTypes s_type, int val)
    {
      int maxUnitCondition = (int) Unit.MAX_UNIT_CONDITION;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null)
        this.ImageCond.ImageIndex = 0 > maxUnitCondition || maxUnitCondition >= this.ImageCond.Images.Length ? this.ImageCond.Images.Length - 1 : maxUnitCondition;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      string str = string.Format(LocalizedText.Get(this.mStrShieldDesc[(int) s_type]), (object) val);
      this.TextValue.text = string.Format(LocalizedText.Get("quest.BUD_COND_SHIELD_DETAIL"), (object) str);
    }

    public void SetCondForcedTargeting()
    {
      int num = (int) Unit.MAX_UNIT_CONDITION + 1;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null && 0 <= num && num < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = num;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = LocalizedText.Get("quest.BUD_COND_FORCED_TARGETING");
    }

    public void SetCondBeForcedTargeted()
    {
      int num = (int) Unit.MAX_UNIT_CONDITION + 2;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null && 0 <= num && num < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = num;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = LocalizedText.Get("quest.BUD_COND_BE_FORCED_TARGETED");
    }

    public void SetCondProtect()
    {
      int num = (int) Unit.MAX_UNIT_CONDITION + 3;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null && 0 <= num && num < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = num;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = LocalizedText.Get("quest.BUD_COND_PROTECT");
    }

    public void SetCondGuard()
    {
      int num = (int) Unit.MAX_UNIT_CONDITION + 4;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && this.ImageCond.Images != null && 0 <= num && num < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = num;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = LocalizedText.Get("quest.BUD_COND_GUARD");
    }
  }
}
