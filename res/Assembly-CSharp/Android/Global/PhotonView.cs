// Decompiled with JetBrains decompiler
// Type: PhotonView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[AddComponentMenu("Photon Networking/Photon View &v")]
public class PhotonView : Photon.MonoBehaviour
{
  public int prefixBackup = -1;
  public OnSerializeTransform onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;
  public OnSerializeRigidBody onSerializeRigidBodyOption = OnSerializeRigidBody.All;
  private Dictionary<Component, MethodInfo> m_OnSerializeMethodInfos = new Dictionary<Component, MethodInfo>(3);
  public int currentMasterID = -1;
  public int ownerId;
  public int group;
  protected internal bool mixedModeIsReliable;
  public bool OwnerShipWasTransfered;
  internal object[] instantiationDataField;
  protected internal object[] lastOnSerializeDataSent;
  protected internal object[] lastOnSerializeDataReceived;
  public ViewSynchronization synchronization;
  public OwnershipOption ownershipTransfer;
  public List<Component> ObservedComponents;
  [SerializeField]
  private int viewIdField;
  public int instantiationId;
  protected internal bool didAwake;
  [SerializeField]
  protected internal bool isRuntimeInstantiated;
  protected internal bool removedFromLocalViewList;
  internal UnityEngine.MonoBehaviour[] RpcMonoBehaviours;
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
    set
    {
      this.prefixBackup = value;
    }
  }

  public object[] instantiationData
  {
    get
    {
      if (!this.didAwake)
        this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
      return this.instantiationDataField;
    }
    set
    {
      this.instantiationDataField = value;
    }
  }

  public int viewID
  {
    get
    {
      return this.viewIdField;
    }
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

  public bool isSceneView
  {
    get
    {
      return this.CreatorActorNr == 0;
    }
  }

  public PhotonPlayer owner
  {
    get
    {
      return PhotonPlayer.Find(this.ownerId);
    }
  }

  public int OwnerActorNr
  {
    get
    {
      return this.ownerId;
    }
  }

  public bool isOwnerActive
  {
    get
    {
      if (this.ownerId != 0)
        return PhotonNetwork.networkingPeer.mActors.ContainsKey(this.ownerId);
      return false;
    }
  }

  public int CreatorActorNr
  {
    get
    {
      return this.viewIdField / PhotonNetwork.MAX_VIEW_IDS;
    }
  }

  public bool isMine
  {
    get
    {
      if (this.ownerId == PhotonNetwork.player.ID)
        return true;
      if (!this.isOwnerActive)
        return PhotonNetwork.isMasterClient;
      return false;
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

  public void TransferOwnership(PhotonPlayer newOwner)
  {
    this.TransferOwnership(newOwner.ID);
  }

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
    if (this.removedFromLocalViewList || (!PhotonNetwork.networkingPeer.LocalCleanPhotonView(this) || (this.instantiationId <= 0 || PhotonHandler.AppQuits) || PhotonNetwork.logLevel < PhotonLogLevel.Informational))
      return;
    Debug.Log((object) ("PUN-instantiated '" + this.gameObject.name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy()."));
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

  protected internal void DeserializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
  {
    if ((Object) component == (Object) null)
      return;
    if (component is UnityEngine.MonoBehaviour)
      this.ExecuteComponentOnSerialize(component, stream, info);
    else if (component is Transform)
    {
      Transform transform = (Transform) component;
      switch (this.onSerializeTransformOption)
      {
        case OnSerializeTransform.OnlyPosition:
          transform.localPosition = (Vector3) stream.ReceiveNext();
          break;
        case OnSerializeTransform.OnlyRotation:
          transform.localRotation = (Quaternion) stream.ReceiveNext();
          break;
        case OnSerializeTransform.OnlyScale:
          transform.localScale = (Vector3) stream.ReceiveNext();
          break;
        case OnSerializeTransform.PositionAndRotation:
          transform.localPosition = (Vector3) stream.ReceiveNext();
          transform.localRotation = (Quaternion) stream.ReceiveNext();
          break;
        case OnSerializeTransform.All:
          transform.localPosition = (Vector3) stream.ReceiveNext();
          transform.localRotation = (Quaternion) stream.ReceiveNext();
          transform.localScale = (Vector3) stream.ReceiveNext();
          break;
      }
    }
    else if (component is Rigidbody)
    {
      Rigidbody rigidbody = (Rigidbody) component;
      switch (this.onSerializeRigidBodyOption)
      {
        case OnSerializeRigidBody.OnlyVelocity:
          rigidbody.velocity = (Vector3) stream.ReceiveNext();
          break;
        case OnSerializeRigidBody.OnlyAngularVelocity:
          rigidbody.angularVelocity = (Vector3) stream.ReceiveNext();
          break;
        case OnSerializeRigidBody.All:
          rigidbody.velocity = (Vector3) stream.ReceiveNext();
          rigidbody.angularVelocity = (Vector3) stream.ReceiveNext();
          break;
      }
    }
    else if (component is Rigidbody2D)
    {
      Rigidbody2D rigidbody2D = (Rigidbody2D) component;
      switch (this.onSerializeRigidBodyOption)
      {
        case OnSerializeRigidBody.OnlyVelocity:
          rigidbody2D.velocity = (Vector2) stream.ReceiveNext();
          break;
        case OnSerializeRigidBody.OnlyAngularVelocity:
          rigidbody2D.angularVelocity = (float) stream.ReceiveNext();
          break;
        case OnSerializeRigidBody.All:
          rigidbody2D.velocity = (Vector2) stream.ReceiveNext();
          rigidbody2D.angularVelocity = (float) stream.ReceiveNext();
          break;
      }
    }
    else
      Debug.LogError((object) "Type of observed is unknown when receiving.");
  }

  protected internal void SerializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
  {
    if ((Object) component == (Object) null)
      return;
    if (component is UnityEngine.MonoBehaviour)
      this.ExecuteComponentOnSerialize(component, stream, info);
    else if (component is Transform)
    {
      Transform transform = (Transform) component;
      switch (this.onSerializeTransformOption)
      {
        case OnSerializeTransform.OnlyPosition:
          stream.SendNext((object) transform.localPosition);
          break;
        case OnSerializeTransform.OnlyRotation:
          stream.SendNext((object) transform.localRotation);
          break;
        case OnSerializeTransform.OnlyScale:
          stream.SendNext((object) transform.localScale);
          break;
        case OnSerializeTransform.PositionAndRotation:
          stream.SendNext((object) transform.localPosition);
          stream.SendNext((object) transform.localRotation);
          break;
        case OnSerializeTransform.All:
          stream.SendNext((object) transform.localPosition);
          stream.SendNext((object) transform.localRotation);
          stream.SendNext((object) transform.localScale);
          break;
      }
    }
    else if (component is Rigidbody)
    {
      Rigidbody rigidbody = (Rigidbody) component;
      switch (this.onSerializeRigidBodyOption)
      {
        case OnSerializeRigidBody.OnlyVelocity:
          stream.SendNext((object) rigidbody.velocity);
          break;
        case OnSerializeRigidBody.OnlyAngularVelocity:
          stream.SendNext((object) rigidbody.angularVelocity);
          break;
        case OnSerializeRigidBody.All:
          stream.SendNext((object) rigidbody.velocity);
          stream.SendNext((object) rigidbody.angularVelocity);
          break;
      }
    }
    else if (component is Rigidbody2D)
    {
      Rigidbody2D rigidbody2D = (Rigidbody2D) component;
      switch (this.onSerializeRigidBodyOption)
      {
        case OnSerializeRigidBody.OnlyVelocity:
          stream.SendNext((object) rigidbody2D.velocity);
          break;
        case OnSerializeRigidBody.OnlyAngularVelocity:
          stream.SendNext((object) rigidbody2D.angularVelocity);
          break;
        case OnSerializeRigidBody.All:
          stream.SendNext((object) rigidbody2D.velocity);
          stream.SendNext((object) rigidbody2D.angularVelocity);
          break;
      }
    }
    else
      Debug.LogError((object) ("Observed type is not serializable: " + (object) ((object) component).GetType()));
  }

  protected internal void ExecuteComponentOnSerialize(Component component, PhotonStream stream, PhotonMessageInfo info)
  {
    IPunObservable punObservable = component as IPunObservable;
    if (punObservable != null)
    {
      punObservable.OnPhotonSerializeView(stream, info);
    }
    else
    {
      if (!((Object) component != (Object) null))
        return;
      MethodInfo mi = (MethodInfo) null;
      if (!this.m_OnSerializeMethodInfos.TryGetValue(component, out mi))
      {
        if (!NetworkingPeer.GetMethod(component as UnityEngine.MonoBehaviour, PhotonNetworkingMessage.OnPhotonSerializeView.ToString(), out mi))
        {
          Debug.LogError((object) ("The observed monobehaviour (" + component.name + ") of this PhotonView does not implement OnPhotonSerializeView()!"));
          mi = (MethodInfo) null;
        }
        this.m_OnSerializeMethodInfos.Add(component, mi);
      }
      if ((object) mi == null)
        return;
      mi.Invoke((object) component, new object[2]
      {
        (object) stream,
        (object) info
      });
    }
  }

  public void RefreshRpcMonoBehaviourCache()
  {
    this.RpcMonoBehaviours = this.GetComponents<UnityEngine.MonoBehaviour>();
  }

  public void RPC(string methodName, PhotonTargets target, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, target, false, parameters);
  }

  public void RpcSecure(string methodName, PhotonTargets target, bool encrypt, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
  }

  public void RPC(string methodName, PhotonPlayer targetPlayer, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, targetPlayer, false, parameters);
  }

  public void RpcSecure(string methodName, PhotonPlayer targetPlayer, bool encrypt, params object[] parameters)
  {
    PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
  }

  public static PhotonView Get(Component component)
  {
    return component.GetComponent<PhotonView>();
  }

  public static PhotonView Get(GameObject gameObj)
  {
    return gameObj.GetComponent<PhotonView>();
  }

  public static PhotonView Find(int viewID)
  {
    return PhotonNetwork.networkingPeer.GetPhotonView(viewID);
  }

  public override string ToString()
  {
    return string.Format("View ({3}){0} on {1} {2}", new object[4]
    {
      (object) this.viewID,
      !((Object) this.gameObject != (Object) null) ? (object) "GO==null" : (object) this.gameObject.name,
      !this.isSceneView ? (object) string.Empty : (object) "(scene)",
      (object) this.prefix
    });
  }
}
