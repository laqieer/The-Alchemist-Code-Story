﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MapHeight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
