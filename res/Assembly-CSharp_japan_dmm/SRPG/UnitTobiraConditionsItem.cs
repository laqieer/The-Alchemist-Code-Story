// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraConditionsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitTobiraConditionsItem : MonoBehaviour
  {
    [SerializeField]
    private ImageArray m_ClearIcon;
    [SerializeField]
    private Text m_ConditionsText;
    [SerializeField]
    private UnitTobiraConditionsItem.TextColor m_TextColor;

    public void Setup(ConditionsResult conds)
    {
      if (conds == null)
      {
        this.SetConditionsText(LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING"));
        this.SetClearIcon(true);
      }
      else
      {
        this.SetConditionsText(conds.text);
        this.SetClearIcon(conds.isClear);
      }
    }

    public void SetConditionsText(string text)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ConditionsText, (UnityEngine.Object) null))
        return;
      this.m_ConditionsText.text = text;
    }

    public void SetClearIcon(bool isClear)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ClearIcon, (UnityEngine.Object) null))
        return;
      if (isClear)
      {
        this.m_ClearIcon.ImageIndex = 1;
        ((Graphic) this.m_ConditionsText).color = this.m_TextColor.m_Clear;
      }
      else
      {
        this.m_ClearIcon.ImageIndex = 0;
        ((Graphic) this.m_ConditionsText).color = this.m_TextColor.m_NotClear;
      }
    }

    [Serializable]
    private class TextColor
    {
      public Color m_Clear = Color.black;
      public Color m_NotClear = Color.black;
    }
  }
}
