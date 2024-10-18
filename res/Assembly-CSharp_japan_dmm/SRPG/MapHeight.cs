// Decompiled with JetBrains decompiler
// Type: SRPG.MapHeight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MapHeight : MonoBehaviour
  {
    private int oldHeight;
    public int Height;
    public BitmapText MapHeightText;
    private Unit mFocusUnit;

    public Unit FocusUnit
    {
      set => this.mFocusUnit = value;
    }

    private void Start() => ((Text) this.MapHeightText).text = this.Height.ToString();

    private void Update()
    {
      if (this.mFocusUnit != null)
        this.Height = SceneBattle.Instance.GetDisplayHeight(this.mFocusUnit);
      if (this.oldHeight != this.Height)
        ((Text) this.MapHeightText).text = this.Height.ToString();
      this.oldHeight = this.Height;
    }
  }
}
