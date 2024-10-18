// Decompiled with JetBrains decompiler
// Type: SRPG.LText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LText : Text
  {
    private string mCurrentText;

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
      this.text = LocalizedText.Get(this.text);
      if (this.supportRichText)
        this.text = LocalizedText.ReplaceTag(this.text);
      this.mCurrentText = this.text;
    }
  }
}
