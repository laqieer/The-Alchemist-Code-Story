// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PhotonTransformViewScaleControl
{
  private Vector3 m_NetworkScale = Vector3.one;
  private PhotonTransformViewScaleModel m_Model;

  public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
  {
    this.m_Model = model;
  }

  public Vector3 GetNetworkScale()
  {
    return this.m_NetworkScale;
  }

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

  public void OnPhotonSerializeView(Vector3 currentScale, PhotonStream stream, PhotonMessageInfo info)
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
