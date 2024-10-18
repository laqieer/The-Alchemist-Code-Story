// Decompiled with JetBrains decompiler
// Type: PhotonView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
[AddComponentMenu("Photon Networking/Photon View &v")]
public class PhotonView : MonoBehaviour
{
  public int ownerId;
  public byte group;
  protected internal bool mixedModeIsReliable;
  public bool OwnerShipWasTransfered;
  public int prefixBackup = -1;
  internal object[] instantiationDataField;
  protected internal object[] lastOnSerializeDataSent;
  protected internal object[] lastOnSerializeDataReceived;
  public ViewSynchronization synchronization;
  public OnSerializeTransform onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;
  public OnSerializeRigidBody onSerializeRigidBodyOption = OnSerializeRigidBody.All;
  public OwnershipOption ownershipTransfer;
  public List<Component> ObservedComponents;
  private Dictionary<Component, MethodInfo> m_OnSerializeMethodInfos = new Dictionary<Component, MethodInfo>(3);
  [SerializeField]
  private int viewIdField;
  public int instantiationId;
  public int currentMasterID = -1;
  protected internal bool didAwake;
  [SerializeField]
  protected internal bool isRuntimeInstantiated;
  protected internal bool removedFromLocalViewList;
  internal MonoBehaviour[] RpcMonoBehaviours;
  private MethodInfo OnSerializeMethodInfo;
  private bool failedToFindOnSerialize;

  public int prefix
  {
    get
    {
      if (this.prefixBackup == -1 && PhotonNetwork.networkingPeer != null)
        this.prefixBackup = (int) PhotonNetwork.networkingPeer.currentLevelPrefix;
      return this.prefixBackup;
    }
    set => this.prefixBackup = value;
  }

  public object[] instantiationData
  {
    get
    {
      if (!this.didAwake)
        this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
      return this.instantiationDataField;
    }
    set => this.instantiationDataField = value;
  }

  public int viewID
  {
    get => this.viewIdField;
    set
    {
      bool flag = this.didAwake && this.viewIdField == 0;
      this.ownerId = value / PhotonNetwork.MAX_VIEW_IDS;
      this.viewIdField = value;
      if (!flag)
        return;
      PhotonNetwork.networkingPeer.RegisterPhotonView(this);
    }
  }

  public bool isSceneView => this.CreatorActorNr == 0;

  public PhotonPlayer owner => PhotonPlayer.Find(this.ownerId);

  public int OwnerActorNr => this.ownerId;

  public bool isOwnerActive
  {
    get => this.ownerId != 0 && PhotonNetwork.networkingPeer.mActors.ContainsKey(this.ownerId);
  }

  public int CreatorActorNr => this.viewIdField / PhotonNetwork.MAX_VIEW_IDS;

  public bool isMine
  {
    get
    {
      if (this.ownerId == PhotonNetwork.player.ID)
        return true;
      return !this.isOwnerActive && PhotonNetwork.isMasterClient;
    }
  }

  protected internal void Awake()
  {
    if (this.viewID != 0)
    {
      PhotonNetwork.networkingPeer.RegisterPhotonView(this);
      this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
    }
    this.didAwake = true;
  }

  public void RequestOwnership()
  {
    PhotonNetwork.networkingPeer.RequestOwnership(this.viewID, this.ownerId);
  }

  public void TransferOwnership(PhotonPlayer newOwner) => this.TransferOwnership(newOwner.ID);

  public void TransferOwnership(int newOwnerId)
  {
    PhotonNetwork.networkingPeer.TransferOwnership(this.viewID, newOwnerId);
    this.ownerId = newOwnerId;
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    if (this.CreatorActorNr == 0 && !this.OwnerShipWasTransfered && (this.currentMasterID == -1 || this.ownerId == this.currentMasterID))
      this.ownerId = newMasterClient.ID;
    this.currentMasterID = newMasterClient.ID;
  }

  protected internal void OnDestroy()
  {
    if (this.removedFromLocalViewList)
      return;
    bool flag1 = PhotonNetwork.networkingPeer.LocalCleanPhotonView(this);
    bool flag2 = false;
    if (!flag1 || flag2 || this.instantiationId <= 0 || PhotonHandler.AppQuits || PhotonNetwork.logLevel < PhotonLogLevel.Informational)
      return;
    Debug.Log((object) ("PUN-instantiated '" + ((Object) ((Component) this).gameObject).name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy()."));
  }

  public void SerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (this.ObservedComponents == null || this.ObservedComponents.Count <= 0)
      return;
    for (int index = 0; index < this.ObservedComponents.Count; ++index)
      this.SerializeComponent(this.ObservedComponents[index], stream, info);
  }

  public void DeserializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (this.ObservedComponents == null || this.ObservedComponents.Count <= 0)
      return;
    for (int index = 0; index < this.ObservedComponents.Count; ++index)
      this.DeserializeComponent(this.ObservedComponents[index], stream, info);
  }

  protected internal void DeserializeComponent(
    Component component,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    switch (component)
    {
      case MonoBehaviour _:
        this.ExecuteComponentOnSerialize(component, stream, info);
        break;
      case Transform _:
        Transform transform = (Transform) component;
        switch (this.onSerializeTransformOption)
        {
          case OnSerializeTransform.OnlyPosition:
            transform.localPosition = (Vector3) stream.ReceiveNext();
            return;
          case OnSerializeTransform.OnlyRotation:
            transform.localRotation = (Quaternion) stream.ReceiveNext();
            return;
          case OnSerializeTransform.OnlyScale:
            transform.localScale = (Vector3) stream.ReceiveNext();
            return;
          case OnSerializeTransform.PositionAndRotation:
            transform.localPosition = (Vector3) stream.ReceiveNext();
            transform.localRotation = (Quaternion) stream.ReceiveNext();
            return;
          case OnSerializeTransform.All:
            transform.localPosition = (Vector3) stream.ReceiveNext();
            transform.localRotation = (Quaternion) stream.ReceiveNext();
            transform.localScale = (Vector3) stream.ReceiveNext();
            return;
          default:
            return;
        }
      case Rigidbody _:
        Rigidbody rigidbody = (Rigidbody) component;
        switch (this.onSerializeRigidBodyOption)
        {
          case OnSerializeRigidBody.OnlyVelocity:
            rigidbody.velocity = (Vector3) stream.ReceiveNext();
            return;
          case OnSerializeRigidBody.OnlyAngularVelocity:
            rigidbody.angularVelocity = (Vector3) stream.ReceiveNext();
            return;
          case OnSerializeRigidBody.All:
            rigidbody.velocity = (Vector3) stream.ReceiveNext();
            rigidbody.angularVelocity = (Vector3) stream.ReceiveNext();
            return;
          default:
            return;
        }
      case Rigidbody2D _:
        Rigidbody2D rigidbody2D = (Rigidbody2D) component;
        switch (this.onSerializeRigidBodyOption)
        {
          case OnSerializeRigidBody.OnlyVelocity:
            rigidbody2D.velocity = (Vector2) stream.ReceiveNext();
            return;
          case OnSerializeRigidBody.OnlyAngularVelocity:
            rigidbody2D.angularVelocity = (float) stream.ReceiveNext();
            return;
          case OnSerializeRigidBody.All:
            rigidbody2D.velocity = (Vector2) stream.ReceiveNext();
            rigidbody2D.angularVelocity = (float) stream.ReceiveNext();
            return;
          default:
            return;
        }
      default:
        Debug.LogError((object) "Type of observed is unknown when receiving.");
        break;
    }
  }

  protected internal void SerializeComponent(
    Component component,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    switch (component)
    {
      case MonoBehaviour _:
        this.ExecuteComponentOnSerialize(component, stream, info);
        break;
      case Transform _:
        Transform transform = (Transform) component;
        switch (this.onSerializeTransformOption)
        {
          case OnSerializeTransform.OnlyPosition:
            stream.SendNext((object) transform.localPosition);
            return;
          case OnSerializeTransform.OnlyRotation:
            stream.SendNext((object) transform.localRotation);
            return;
          case OnSerializeTransform.OnlyScale:
            stream.SendNext((object) transform.localScale);
            return;
          case OnSerializeTransform.PositionAndRotation:
            stream.SendNext((object) transform.localPosition);
            stream.SendNext((object) transform.localRotation);
            return;
          case OnSerializeTransform.All:
            stream.SendNext((object) transform.localPosition);
            stream.SendNext((object) transform.localRotation);
            stream.SendNext((object) transform.localScale);
            return;
          default:
            return;
        }
      case Rigidbody _:
        Rigidbody rigidbody = (Rigidbody) component;
        switch (this.onSerializeRigidBodyOption)
        {
          case OnSerializeRigidBody.OnlyVelocity:
            stream.SendNext((object) rigidbody.velocity);
            return;
          case OnSerializeRigidBody.OnlyAngularVelocity:
            stream.SendNext((object) rigidbody.angularVelocity);
            return;
          case OnSerializeRigidBody.All:
            stream.SendNext((object) rigidbody.velocity);
            stream.SendNext((object) rigidbody.angularVelocity);
            return;
          default:
            return;
        }
      case Rigidbody2D _:
        Rigidbody2D rigidbody2D = (Rigidbody2D) component;
        switch (this.onSerializeRigidBodyOption)
        {
          case OnSerializeRigidBody.OnlyVelocity:
            stream.SendNext((object) rigidbody2D.velocity);
            return;
          case OnSerializeRigidBody.OnlyAngularVelocity:
            stream.SendNext((object) rigidbody2D.angularVelocity);
            return;
          case OnSerializeRigidBody.All:
            stream.SendNext((object) rigidbody2D.velocity);
            stream.SendNext((object) rigidbody2D.angularVelocity);
            return;
          default:
            return;
        }
      default:
        Debug.LogError((object) ("Observed type is not serializable: " + (object) component.GetType()));
        break;
    }
  }

  protected internal void ExecuteComponentOnSerialize(
    Component component,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (component is IPunObservable punObservable)
    {
      punObservable.OnPhotonSerializeView(stream, info);
    }
    else
    {
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      MethodInfo mi = (MethodInfo) null;
      if (!this.m_OnSerializeMethodInfos.TryGetValue(component, out mi))
      {
        if (!NetworkingPeer.GetMethod(component as MonoBehaviour, PhotonNetworkingMessage.OnPhotonSerializeView.ToString(), out mi))
        {
          Debug.LogError((object) ("The observed monobehaviour (" + ((Object) component).name + ") of this PhotonView does not implement OnPhotonSerializeView()!"));
          mi = (MethodInfo) null;
        }
        this.m_OnSerializeMethodInfos.Add(component, mi);
      }
      mi?.Invoke((object) component, new object[2]
      {
        (object) stream,
        (object) info
      });
    }
  }

  public void RefreshRpcMonoBehaviourCache()
  {
    this.RpcMonoBehaviours = ((Component) this).GetComponents<MonoBehaviour>();
  }

  public void RPC(string methodName, PhotonTargets target, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, target, false, parameters);
  }

  public void RpcSecure(
    string methodName,
    PhotonTargets target,
    bool encrypt,
    params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
  }

  public void RPC(string methodName, PhotonPlayer targetPlayer, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, targetPlayer, false, parameters);
  }

  public void RpcSecure(
    string methodName,
    PhotonPlayer targetPlayer,
    bool encrypt,
    params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
  }

  public static PhotonView Get(Component component) => component.GetComponent<PhotonView>();

  public static PhotonView Get(GameObject gameObj) => gameObj.GetComponent<PhotonView>();

  public static PhotonView Find(int viewID) => PhotonNetwork.networkingPeer.GetPhotonView(viewID);

  public virtual string ToString()
  {
    return string.Format("View ({3}){0} on {1} {2}", (object) this.viewID, !Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null) ? (object) "GO==null" : (object) ((Object) ((Component) this).gameObject).name, !this.isSceneView ? (object) string.Empty : (object) "(scene)", (object) this.prefix);
  }
}
