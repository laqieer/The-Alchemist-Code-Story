// Decompiled with JetBrains decompiler
// Type: CurveAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class CurveAsset : ScriptableObject
{
  public CurveAsset.CurveStruct[] Curves = new CurveAsset.CurveStruct[0];

  public AnimationCurve FindCurve(string name)
  {
    for (int index = this.Curves.Length - 1; index >= 0; --index)
    {
      if (this.Curves[index].Name == name)
        return this.Curves[index].Curve;
    }
    return (AnimationCurve) null;
  }

  [Serializable]
  public struct CurveStruct
  {
    public string Name;
    public AnimationCurve Curve;
  }
}
