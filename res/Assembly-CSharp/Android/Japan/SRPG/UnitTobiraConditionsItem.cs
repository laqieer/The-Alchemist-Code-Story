// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraConditionsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

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
      if (!((UnityEngine.Object) this.m_ConditionsText != (UnityEngine.Object) null))
        return;
      this.m_ConditionsText.text = text;
    }

    public void SetClearIcon(bool isClear)
    {
      if (!((UnityEngine.Object) this.m_ClearIcon != (UnityEngine.Object) null))
        return;
      if (isClear)
      {
        this.m_ClearIcon.ImageIndex = 1;
        this.m_ConditionsText.color = this.m_TextColor.m_Clear;
      }
      else
      {
        this.m_ClearIcon.ImageIndex = 0;
        this.m_ConditionsText.color = this.m_TextColor.m_NotClear;
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
