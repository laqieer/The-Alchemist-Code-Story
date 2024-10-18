// Decompiled with JetBrains decompiler
// Type: SRPG.ContentGrid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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

    public static ContentGrid zero => new ContentGrid(0, 0);

    public float fx
    {
      set => this.x = ContentGrid.FloatToInt(value);
    }

    public float fy
    {
      set => this.y = ContentGrid.FloatToInt(value);
    }

    public static int FloatToInt(float value) => Mathf.FloorToInt(value);

    public override string ToString()
    {
      return string.Format("[Grid: " + (object) this.x + ", " + (object) this.y + "]");
    }
  }
}
