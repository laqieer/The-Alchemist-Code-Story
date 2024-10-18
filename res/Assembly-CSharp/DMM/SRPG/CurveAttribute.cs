// Decompiled with JetBrains decompiler
// Type: SRPG.CurveAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CurveAttribute : PropertyAttribute
  {
    public float PosX;
    public float PosY;
    public float RangeX;
    public float RangeY;
    public bool b;
    public int x;

    public CurveAttribute(float PosX, float PosY, float RangeX, float RangeY, bool b)
    {
      this.PosX = PosX;
      this.PosY = PosY;
      this.RangeX = RangeX;
      this.RangeY = RangeY;
      this.b = b;
    }
  }
}
