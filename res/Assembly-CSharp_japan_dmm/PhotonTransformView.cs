// Decompiled with JetBrains decompiler
// Type: PhotonTransformView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
[AddComponentMenu("Photon Networking/Photon Transform View")]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  public PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();
  [SerializeField]
  public PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();
  [SerializeField]
  public PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();
  private PhotonTransformViewPositionControl m_PositionControl;
  private PhotonTransformViewRotationControl m_RotationControl;
  private PhotonTransformViewScaleControl m_ScaleControl;
  private PhotonView m_PhotonView;
  private bool m_ReceivedNetworkUpdate;
  private bool m_firstTake;

  private void Awake()
  {
    this.m_PhotonView = ((Component) this).GetComponent<PhotonView>();
    this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
    this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
    this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
  }

  private void OnEnable() => this.m_firstTake = true;

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_PhotonView, (Object) null) || this.m_PhotonView.isMine || !PhotonNetwork.connected)
      return;
    this.UpdatePosition();
    this.UpdateRotation();
    this.UpdateScale();
  }

  private void UpdatePosition()
  {
    if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).transform.localPosition = this.m_PositionControl.UpdatePosition(((Component) this).transform.localPosition);
  }

  private void UpdateRotation()
  {
    if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).transform.localRotation = this.m_RotationControl.GetRotation(((Component) this).transform.localRotation);
  }

  private void UpdateScale()
  {
    if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).transform.localScale = this.m_ScaleControl.GetScale(((Component) this).transform.localScale);
  }

  public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
  {
    this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    this.m_PositionControl.OnPhotonSerializeView(((Component) this).transform.localPosition, stream, info);
    this.m_RotationControl.OnPhotonSerializeView(((Component) this).transform.localRotation, stream, info);
    this.m_ScaleControl.OnPhotonSerializeView(((Component) this).transform.localScale, stream, info);
    if (!this.m_PhotonView.isMine && this.m_PositionModel.DrawErrorGizmo)
      this.DoDrawEstimatedPositionError();
    if (!stream.isReading)
      return;
    this.m_ReceivedNetworkUpdate = true;
    if (!this.m_firstTake)
      return;
    this.m_firstTake = false;
    if (this.m_PositionModel.SynchronizeEnabled)
      ((Component) this).transform.localPosition = this.m_PositionControl.GetNetworkPosition();
    if (this.m_RotationModel.SynchronizeEnabled)
      ((Component) this).transform.localRotation = this.m_RotationControl.GetNetworkRotation();
    if (!this.m_ScaleModel.SynchronizeEnabled)
      return;
    ((Component) this).transform.localScale = this.m_ScaleControl.GetNetworkScale();
  }

  private void DoDrawEstimatedPositionError()
  {
    Vector3 vector3 = this.m_PositionControl.GetNetworkPosition();
    if (Object.op_Inequality((Object) ((Component) this).transform.parent, (Object) null))
      vector3 = Vector3.op_Addition(((Component) this).transform.parent.position, vector3);
    Debug.DrawLine(vector3, ((Component) this).transform.position, Color.red, 2f);
    Debug.DrawLine(((Component) this).transform.position, Vector3.op_Addition(((Component) this).transform.position, Vector3.up), Color.green, 2f);
    Debug.DrawLine(vector3, Vector3.op_Addition(vector3, Vector3.up), Color.red, 2f);
  }
}
