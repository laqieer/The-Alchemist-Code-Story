// Decompiled with JetBrains decompiler
// Type: SRPG.LText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      this.mCurrentText = this.text;
    }
  }
}
