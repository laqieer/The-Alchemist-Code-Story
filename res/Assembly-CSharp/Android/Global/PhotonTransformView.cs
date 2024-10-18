// Decompiled with JetBrains decompiler
// Type: PhotonTransformView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Photon Networking/Photon Transform View")]
[RequireComponent(typeof (PhotonView))]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  private PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();
  [SerializeField]
  private PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();
  [SerializeField]
  private PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();
  private PhotonTransformViewPositionControl m_PositionControl;
  private PhotonTransformViewRotationControl m_RotationControl;
  private PhotonTransformViewScaleControl m_ScaleControl;
  private PhotonView m_PhotonView;
  private bool m_ReceivedNetworkUpdate;
  private bool m_firstTake;

  private void Awake()
  {
    this.m_PhotonView = this.GetComponent<PhotonView>();
    this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
    this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
    this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
  }

  private void OnEnable()
  {
    this.m_firstTake = true;
  }

  private void Update()
  {
    if ((Object) this.m_PhotonView == (Object) null || this.m_PhotonView.isMine || !PhotonNetwork.connected)
      return;
    this.UpdatePosition();
    this.UpdateRotation();
    this.UpdateScale();
  }

  private void UpdatePosition()
  {
    if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    this.transform.localPosition = this.m_PositionControl.UpdatePosition(this.transform.localPosition);
  }

  private void UpdateRotation()
  {
    if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    this.transform.localRotation = this.m_RotationControl.GetRotation(this.transform.localRotation);
  }

  private void UpdateScale()
  {
    if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    this.transform.localScale = this.m_ScaleControl.GetScale(this.transform.localScale);
  }

  public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
  {
    this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    this.m_PositionControl.OnPhotonSerializeView(this.transform.localPosition, stream, info);
    this.m_RotationControl.OnPhotonSerializeView(this.transform.localRotation, stream, info);
    this.m_ScaleControl.OnPhotonSerializeView(this.transform.localScale, stream, info);
    if (!this.m_PhotonView.isMine && this.m_PositionModel.DrawErrorGizmo)
      this.DoDrawEstimatedPositionError();
    if (!stream.isReading)
      return;
    this.m_ReceivedNetworkUpdate = true;
    if (!this.m_firstTake)
      return;
    this.m_firstTake = false;
    if (this.m_PositionModel.SynchronizeEnabled)
      this.transform.localPosition = this.m_PositionControl.GetNetworkPosition();
    if (this.m_RotationModel.SynchronizeEnabled)
      this.transform.localRotation = this.m_RotationControl.GetNetworkRotation();
    if (!this.m_ScaleModel.SynchronizeEnabled)
      return;
    this.transform.localScale = this.m_ScaleControl.GetNetworkScale();
  }

  private void DoDrawEstimatedPositionError()
  {
    Vector3 start = this.m_PositionControl.GetNetworkPosition();
    if ((Object) this.transform.parent != (Object) null)
      start = this.transform.parent.position + start;
    Debug.DrawLine(start, this.transform.position, Color.red, 2f);
    Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up, Color.green, 2f);
    Debug.DrawLine(start, start + Vector3.up, Color.red, 2f);
  }
}
