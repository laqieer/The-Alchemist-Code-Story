// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Serializable]
public class PhotonTransformViewRotationModel
{
  public bool SynchronizeEnabled;
  public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;
  public float InterpolateRotateTowardsSpeed = 180f;
  public float InterpolateLerpSpeed = 5f;

  public enum InterpolateOptions
  {
    Disabled,
    RotateTowards,
    Lerp,
  }
}
