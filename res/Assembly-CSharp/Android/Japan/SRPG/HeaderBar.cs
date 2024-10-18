// Decompiled with JetBrains decompiler
// Type: SRPG.HeaderBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
