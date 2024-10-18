﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailAtkDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetailAtkDetail : MonoBehaviour
  {
    public ImageArray ImageAtkDetail;
    public ImageArray ImageFluct;
    public GameObject GoResist;
    public GameObject GoAvoid;
    public UnityEngine.UI.Text TextValue;
    private static string[] mStrAtkDetails = new string[11]
    {
      string.Empty,
      "quest.BUD_AD_SLASH",
      "quest.BUD_AD_STAB",
      "quest.BUD_AD_BLOW",
      "quest.BUD_AD_SHOT",
      "quest.BUD_AD_MAGIC",
      "quest.BUD_AD_JUMP",
      "quest.BUD_AD_ALL_HIT",
      "quest.BUD_AD_ALL_AVOID",
      "quest.BUD_AD_ALL_RECOVER",
      "quest.BUD_AD_ALL_CRI_DMG"
    };
    private static string[] mStrTypes = new string[3]
    {
      "quest.BUD_AD_ASSIST",
      "quest.BUD_AD_RESIST",
      "quest.BUD_AD_AVOID"
    };

    public void SetAll(
      BattleUnitDetailAtkDetail.eAllType all_type,
      BattleUnitDetail.eBudFluct fluct)
    {
      if (Object.op_Implicit((Object) this.ImageAtkDetail))
      {
        int num = (int) all_type;
        if (num >= 0 && num < this.ImageAtkDetail.Images.Length)
          this.ImageAtkDetail.ImageIndex = num;
      }
      if (Object.op_Implicit((Object) this.GoResist))
        this.GoResist.SetActive(false);
      if (Object.op_Implicit((Object) this.GoAvoid))
        this.GoAvoid.SetActive(false);
      if (Object.op_Implicit((Object) this.ImageFluct))
      {
        int num = (int) fluct;
        if (num < this.ImageFluct.Images.Length)
          this.ImageFluct.ImageIndex = num;
      }
      if (!Object.op_Implicit((Object) this.TextValue))
        return;
      StringBuilder sb = new StringBuilder(64);
      sb.Length = 0;
      sb.Append(LocalizedText.Get(BattleUnitDetailAtkDetail.mStrAtkDetails[(int) all_type]));
      this.AddUpDownText(sb, fluct);
      this.TextValue.text = sb.ToString();
    }

    private void AddUpDownText(StringBuilder sb, BattleUnitDetail.eBudFluct fluct)
    {
      if (sb == null)
        return;
      switch (fluct)
      {
        case BattleUnitDetail.eBudFluct.DW_L:
        case BattleUnitDetail.eBudFluct.DW_M:
        case BattleUnitDetail.eBudFluct.DW_S:
          sb.Append(LocalizedText.Get("quest.BUD_AD_DW"));
          break;
        case BattleUnitDetail.eBudFluct.UP_S:
        case BattleUnitDetail.eBudFluct.UP_M:
        case BattleUnitDetail.eBudFluct.UP_L:
          sb.Append(LocalizedText.Get("quest.BUD_AD_UP"));
          break;
      }
    }

    public void SetAtkDetail(
      AttackDetailTypes atk_detail,
      BattleUnitDetailAtkDetail.eType type,
      BattleUnitDetail.eBudFluct fluct)
    {
      if (Object.op_Implicit((Object) this.ImageAtkDetail))
      {
        int num = (int) atk_detail;
        if (num >= 0 && num < this.ImageAtkDetail.Images.Length)
          this.ImageAtkDetail.ImageIndex = num;
      }
      if (Object.op_Implicit((Object) this.GoResist))
        this.GoResist.SetActive(false);
      if (Object.op_Implicit((Object) this.GoAvoid))
        this.GoAvoid.SetActive(false);
      switch (type)
      {
        case BattleUnitDetailAtkDetail.eType.RESIST:
          if (Object.op_Implicit((Object) this.GoResist))
          {
            this.GoResist.SetActive(true);
            break;
          }
          break;
        case BattleUnitDetailAtkDetail.eType.AVOID:
          if (Object.op_Implicit((Object) this.GoAvoid))
          {
            this.GoAvoid.SetActive(true);
            break;
          }
          break;
      }
      if (Object.op_Implicit((Object) this.ImageFluct))
      {
        int num = (int) fluct;
        if (num < this.ImageFluct.Images.Length)
          this.ImageFluct.ImageIndex = num;
      }
      if (!Object.op_Implicit((Object) this.TextValue))
        return;
      StringBuilder sb = new StringBuilder(64);
      sb.Length = 0;
      sb.Append(LocalizedText.Get(BattleUnitDetailAtkDetail.mStrAtkDetails[(int) atk_detail]));
      sb.Append(LocalizedText.Get(BattleUnitDetailAtkDetail.mStrTypes[(int) type]));
      this.AddUpDownText(sb, fluct);
      this.TextValue.text = sb.ToString();
    }

    public enum eType
    {
      ASSIST,
      RESIST,
      AVOID,
      MAX,
    }

    public enum eAllType
    {
      HIT = 7,
      MIN = 7,
      AVOID = 8,
      RECOVER = 9,
      CRI_DMG = 10, // 0x0000000A
      MAX = 11, // 0x0000000B
    }
  }
}
