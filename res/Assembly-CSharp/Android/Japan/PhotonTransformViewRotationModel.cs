// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class PhotonTransformViewRotationModel
{
  public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;
  public float InterpolateRotateTowardsSpeed = 180f;
  public float InterpolateLerpSpeed = 5f;
  public bool SynchronizeEnabled;

  public enum InterpolateOptions
  {
    Disabled,
    RotateTowards,
    Lerp,
  }
}
