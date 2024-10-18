// Decompiled with JetBrains decompiler
// Type: SRPG.NoBreakSpaceText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

namespace SRPG
{
  public class NoBreakSpaceText : Text
  {
    public override string text
    {
      get
      {
        return base.text;
      }
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
