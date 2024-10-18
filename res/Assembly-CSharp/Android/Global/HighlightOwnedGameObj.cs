// Decompiled with JetBrains decompiler
// Type: HighlightOwnedGameObj
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class HighlightOwnedGameObj : Photon.MonoBehaviour
{
  public float Offset = 0.5f;
  public GameObject PointerPrefab;
  private Transform markerTransform;

  private void Update()
  {
    if (this.photonView.isMine)
    {
      if ((Object) this.markerTransform == (Object) null)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.PointerPrefab);
        gameObject.transform.parent = this.gameObject.transform;
        this.markerTransform = gameObject.transform;
      }
      Vector3 position = this.gameObject.transform.position;
      this.markerTransform.position = new Vector3(position.x, position.y + this.Offset, position.z);
      this.markerTransform.rotation = Quaternion.identity;
    }
    else
    {
      if (!((Object) this.markerTransform != (Object) null))
        return;
      Object.Destroy((Object) this.markerTransform.gameObject);
      this.markerTransform = (Transform) null;
    }
  }
}
