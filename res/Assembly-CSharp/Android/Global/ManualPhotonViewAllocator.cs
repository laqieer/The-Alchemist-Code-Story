// Decompiled with JetBrains decompiler
// Type: ManualPhotonViewAllocator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
  public GameObject Prefab;

  public void AllocateManualPhotonView()
  {
    PhotonView photonView = this.gameObject.GetPhotonView();
    if ((Object) photonView == (Object) null)
    {
      Debug.LogError((object) "Can't do manual instantiation without PhotonView component.");
    }
    else
    {
      int num = PhotonNetwork.AllocateViewID();
      photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, new object[1]
      {
        (object) num
      });
    }
  }

  [PunRPC]
  public void InstantiateRpc(int viewID)
  {
    GameObject go = Object.Instantiate((Object) this.Prefab, InputToEvent.inputHitPos + new Vector3(0.0f, 5f, 0.0f), Quaternion.identity) as GameObject;
    go.GetPhotonView().viewID = viewID;
    go.GetComponent<OnClickDestroy>().DestroyByRpc = true;
  }
}
