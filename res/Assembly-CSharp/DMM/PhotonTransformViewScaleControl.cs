// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class PhotonTransformViewScaleControl
{
  private PhotonTransformViewScaleModel m_Model;
  private Vector3 m_NetworkScale = Vector3.one;

  public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
  {
    this.m_Model = model;
  }

  public Vector3 GetNetworkScale() => this.m_NetworkScale;

  public Vector3 GetScale(Vector3 currentScale)
  {
    switch (this.m_Model.InterpolateOption)
    {
      case PhotonTransformViewScaleModel.InterpolateOptions.MoveTowards:
        return Vector3.MoveTowards(currentScale, this.m_NetworkScale, this.m_Model.InterpolateMoveTowardsSpeed * Time.deltaTime);
      case PhotonTransformViewScaleModel.InterpolateOptions.Lerp:
        return Vector3.Lerp(currentScale, this.m_NetworkScale, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
      default:
        return this.m_NetworkScale;
    }
  }

  public void OnPhotonSerializeView(
    Vector3 currentScale,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (!this.m_Model.SynchronizeEnabled)
      return;
    if (stream.isWriting)
    {
      stream.SendNext((object) currentScale);
      this.m_NetworkScale = currentScale;
    }
    else
      this.m_NetworkScale = (Vector3) stream.ReceiveNext();
  }
}
