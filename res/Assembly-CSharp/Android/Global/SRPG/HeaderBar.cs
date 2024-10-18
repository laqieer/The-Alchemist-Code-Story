// Decompiled with JetBrains decompiler
// Type: SRPG.HeaderBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class HeaderBar : PropertyAttribute
  {
    public string Text;
    public Color BGColor;
    public Color FGColor;

    public HeaderBar(string text)
    {
      this.Text = text;
      this.BGColor = new Color(0.0f, 0.2f, 0.5f);
      this.FGColor = Color.white;
    }

    public HeaderBar(string text, Color bg)
    {
      this.Text = text;
      this.BGColor = bg;
      this.FGColor = Color.white;
    }

    public HeaderBar(string text, Color bg, Color fg)
    {
      this.Text = text;
      this.BGColor = bg;
      this.FGColor = fg;
    }
  }
}
