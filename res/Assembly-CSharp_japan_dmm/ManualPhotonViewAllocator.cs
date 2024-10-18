// Decompiled with JetBrains decompiler
// Type: ManualPhotonViewAllocator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
  public GameObject Prefab;

  public void AllocateManualPhotonView()
  {
    PhotonView photonView = ((Component) this).gameObject.GetPhotonView();
    if (Object.op_Equality((Object) photonView, (Object) null))
    {
      Debug.LogError((object) "Can't do manual instantiation without PhotonView component.");
    }
    else
    {
      int num = PhotonNetwork.AllocateViewID();
      photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, (object) num);
    }
  }

  [PunRPC]
  public void InstantiateRpc(int viewID)
  {
    GameObject go = Object.Instantiate<GameObject>(this.Prefab, Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.identity);
    go.GetPhotonView().viewID = viewID;
    go.GetComponent<OnClickDestroy>().DestroyByRpc = true;
  }
}
