// Decompiled with JetBrains decompiler
// Type: HighlightOwnedGameObj
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Photon;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class HighlightOwnedGameObj : MonoBehaviour
{
  public GameObject PointerPrefab;
  public float Offset = 0.5f;
  private Transform markerTransform;

  private void Update()
  {
    if (this.photonView.isMine)
    {
      if (Object.op_Equality((Object) this.markerTransform, (Object) null))
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.PointerPrefab);
        gameObject.transform.parent = ((Component) this).gameObject.transform;
        this.markerTransform = gameObject.transform;
      }
      Vector3 position = ((Component) this).gameObject.transform.position;
      this.markerTransform.position = new Vector3(position.x, position.y + this.Offset, position.z);
      this.markerTransform.rotation = Quaternion.identity;
    }
    else
    {
      if (!Object.op_Inequality((Object) this.markerTransform, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.markerTransform).gameObject);
      this.markerTransform = (Transform) null;
    }
  }
}
