﻿// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class PhotonTransformViewRotationControl
{
  private PhotonTransformViewRotationModel m_Model;
  private Quaternion m_NetworkRotation;

  public PhotonTransformViewRotationControl(PhotonTransformViewRotationModel model)
  {
    this.m_Model = model;
  }

  public Quaternion GetNetworkRotation() => this.m_NetworkRotation;

  public Quaternion GetRotation(Quaternion currentRotation)
  {
    switch (this.m_Model.InterpolateOption)
    {
      case PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards:
        return Quaternion.RotateTowards(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateRotateTowardsSpeed * Time.deltaTime);
      case PhotonTransformViewRotationModel.InterpolateOptions.Lerp:
        return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
      default:
        return this.m_NetworkRotation;
    }
  }

  public void OnPhotonSerializeView(
    Quaternion currentRotation,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (!this.m_Model.SynchronizeEnabled)
      return;
    if (stream.isWriting)
    {
      stream.SendNext((object) currentRotation);
      this.m_NetworkRotation = currentRotation;
    }
    else
      this.m_NetworkRotation = (Quaternion) stream.ReceiveNext();
  }
}
