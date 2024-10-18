// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Serializable]
public class PhotonTransformViewScaleModel
{
  public bool SynchronizeEnabled;
  public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;
  public float InterpolateMoveTowardsSpeed = 1f;
  public float InterpolateLerpSpeed;

  public enum InterpolateOptions
  {
    Disabled,
    MoveTowards,
    Lerp,
  }
}
