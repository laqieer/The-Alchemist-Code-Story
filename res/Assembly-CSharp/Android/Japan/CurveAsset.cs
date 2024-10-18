// Decompiled with JetBrains decompiler
// Type: CurveAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
