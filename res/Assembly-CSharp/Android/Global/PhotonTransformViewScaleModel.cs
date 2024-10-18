// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

[Serializable]
public class PhotonTransformViewScaleModel
{
  public float InterpolateMoveTowardsSpeed = 1f;
  public bool SynchronizeEnabled;
  public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;
  public float InterpolateLerpSpeed;

  public enum InterpolateOptions
  {
    Disabled,
    MoveTowards,
    Lerp,
  }
}
