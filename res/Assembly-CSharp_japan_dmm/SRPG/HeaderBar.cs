// Decompiled with JetBrains decompiler
// Type: SRPG.HeaderBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
