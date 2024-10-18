// Decompiled with JetBrains decompiler
// Type: ShowInfoOfPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class ShowInfoOfPlayer : Photon.MonoBehaviour
{
  private GameObject textGo;
  private TextMesh tm;
  public float CharacterSize;
  public Font font;
  public bool DisableOnOwnObjects;

  private void Start()
  {
    if ((Object) this.font == (Object) null)
    {
      this.font = (Font) Resources.FindObjectsOfTypeAll(typeof (Font))[0];
      Debug.LogWarning((object) ("No font defined. Found font: " + (object) this.font));
    }
    if (!((Object) this.tm == (Object) null))
      return;
    this.textGo = new GameObject("3d text");
    this.textGo.transform.parent = this.gameObject.transform;
    this.textGo.transform.localPosition = Vector3.zero;
    this.textGo.AddComponent<MeshRenderer>().material = this.font.material;
    this.tm = this.textGo.AddComponent<TextMesh>();
    this.tm.font = this.font;
    this.tm.anchor = TextAnchor.MiddleCenter;
    if ((double) this.CharacterSize <= 0.0)
      return;
    this.tm.characterSize = this.CharacterSize;
  }

  private void Update()
  {
    bool flag = !this.DisableOnOwnObjects || this.photonView.isMine;
    if ((Object) this.textGo != (Object) null)
      this.textGo.SetActive(flag);
    if (!flag)
      return;
    PhotonPlayer owner = this.photonView.owner;
    if (owner != null)
      this.tm.text = !string.IsNullOrEmpty(owner.NickName) ? owner.NickName : "player" + (object) owner.ID;
    else if (this.photonView.isSceneView)
      this.tm.text = "scn";
    else
      this.tm.text = "n/a";
  }
}
