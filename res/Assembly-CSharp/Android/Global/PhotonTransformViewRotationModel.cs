// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
