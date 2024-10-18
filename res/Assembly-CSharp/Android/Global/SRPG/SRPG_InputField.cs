// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_InputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/InputField (SRPG)")]
  public class SRPG_InputField : InputField
  {
    public override void OnPointerEnter(PointerEventData eventData)
    {
      base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
      base.OnPointerExit(eventData);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
      base.OnUpdateSelected(eventData);
    }

    private bool GetMouseButtonDown()
    {
      if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        return Input.GetMouseButtonDown(2);
      return true;
    }

    public virtual void ForceSetText(string text)
    {
      if (this.characterLimit > 0 && text.Length > this.characterLimit)
        text = text.Substring(0, this.characterLimit);
      this.m_Text = text;
      if (this.m_Keyboard != null)
        this.m_Keyboard.text = this.m_Text;
      if (this.m_CaretPosition > this.m_Text.Length)
        this.m_CaretPosition = this.m_CaretSelectPosition = this.m_Text.Length;
      if (this.onValueChanged != null)
        this.onValueChanged.Invoke(text);
      this.UpdateLabel();
    }
  }
}
