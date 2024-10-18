﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ContentGrid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public struct ContentGrid
  {
    public int x;
    public int y;

    public ContentGrid(int _ix, int _iy)
    {
      this.x = _ix;
      this.y = _iy;
    }

    public ContentGrid(float _fx, float _fy)
    {
      this.x = ContentGrid.FloatToInt(_fx);
      this.y = ContentGrid.FloatToInt(_fy);
    }

    public static ContentGrid zero
    {
      get
      {
        return new ContentGrid(0, 0);
      }
    }

    public float fx
    {
      set
      {
        this.x = ContentGrid.FloatToInt(value);
      }
    }

    public float fy
    {
      set
      {
        this.y = ContentGrid.FloatToInt(value);
      }
    }

    public static int FloatToInt(float value)
    {
      return Mathf.FloorToInt(value);
    }

    public override string ToString()
    {
      return string.Format("[Grid: " + (object) this.x + ", " + (object) this.y + "]");
    }
  }
}
