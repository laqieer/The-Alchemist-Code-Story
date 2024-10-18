// Decompiled with JetBrains decompiler
// Type: SRPG.NoBreakSpaceText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class NoBreakSpaceText : Text
  {
    public virtual string text
    {
      get => base.text;
      set
      {
        base.text = value;
        this.Refresh();
      }
    }

    public void Refresh()
    {
      this.m_Text = this.m_Text.Replace(" ", Convert.ToChar(Convert.ToInt32("00A0", 16)).ToString());
    }
  }
}
