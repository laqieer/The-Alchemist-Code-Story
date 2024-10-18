﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SkillRange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public struct SkillRange
  {
    private int w;
    private int h;
    private int[] bit;
    private int count;

    public SkillRange(int _w, int _h)
    {
      if (_w > 32)
      {
        Debug.LogError((object) "横32以上は未対応");
        this.w = 32;
      }
      this.w = _w;
      this.h = _h;
      this.bit = new int[this.h];
      this.count = 0;
    }

    public void Clear()
    {
      for (int index = 0; index < this.bit.Length; ++index)
        this.bit[index] = 0;
      this.count = 0;
    }

    public void Set(int x, int y)
    {
      if (x < 0 || y < 0 || x >= this.w || y >= this.h)
      {
        Debug.LogError((object) ("failed range over > x=" + (object) x + ", y=" + (object) y));
      }
      else
      {
        if (this.Get(x, y))
          return;
        this.bit[y] |= 1 << x;
        ++this.count;
      }
    }

    public bool Get(int x, int y)
    {
      if (x >= 0 && y >= 0 && x < this.w && y < this.h)
        return (this.bit[y] & 1 << x) > 0;
      Debug.LogError((object) ("failed range over > x=" + (object) x + ", y=" + (object) y));
      return false;
    }

    public int Count() => this.count;
  }
}
