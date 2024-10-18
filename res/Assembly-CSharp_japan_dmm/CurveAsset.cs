// Decompiled with JetBrains decompiler
// Type: CurveAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
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
