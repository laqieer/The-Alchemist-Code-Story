// Decompiled with JetBrains decompiler
// Type: SRPG.LText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class LText : Text
  {
    [HideInInspector]
    [SerializeField]
    private bool mReplaceNoBreakSpace;
    private string mCurrentText;

    public bool ReplaceNoBreakSpace
    {
      get => this.mReplaceNoBreakSpace;
      set
      {
        if (value == this.mReplaceNoBreakSpace)
          return;
        this.mReplaceNoBreakSpace = value;
        this.ReplaceLocalizedText();
      }
    }

    private void LateUpdate()
    {
      if (!Application.isPlaying)
        return;
      if (string.IsNullOrEmpty(this.mCurrentText))
      {
        if (string.IsNullOrEmpty(this.text))
          return;
      }
      else if (!string.IsNullOrEmpty(this.text) && this.mCurrentText.Equals(this.text))
        return;
      this.ReplaceLocalizedText();
    }

    private void ReplaceLocalizedText()
    {
      if (!Application.isPlaying)
        return;
      string str = LocalizedText.Get(this.text);
      if (this.ReplaceNoBreakSpace)
        str = str.Replace(' ', ' ');
      this.text = str;
      this.mCurrentText = this.text;
    }
  }
}
