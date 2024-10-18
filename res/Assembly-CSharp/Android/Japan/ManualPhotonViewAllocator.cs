// Decompiled with JetBrains decompiler
// Type: ManualPhotonViewAllocator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    GameObject go = Object.Instantiate<GameObject>(this.Prefab, InputToEvent.inputHitPos + new Vector3(0.0f, 5f, 0.0f), Quaternion.identity);
    go.GetPhotonView().viewID = viewID;
    go.GetComponent<OnClickDestroy>().DestroyByRpc = true;
  }
}
