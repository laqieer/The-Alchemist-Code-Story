// Decompiled with JetBrains decompiler
// Type: SRPG.MapHeight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
      set
      {
        this.mFocusUnit = value;
      }
    }

    private void Start()
    {
      this.MapHeightText.text = this.Height.ToString();
    }

    private void Update()
    {
      if (this.mFocusUnit != null)
        this.Height = SceneBattle.Instance.GetDisplayHeight(this.mFocusUnit);
      if (this.oldHeight != this.Height)
        this.MapHeightText.text = this.Height.ToString();
      this.oldHeight = this.Height;
    }
  }
}
