// Decompiled with JetBrains decompiler
// Type: OnClickInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class OnClickInstantiate : MonoBehaviour
{
  private string[] InstantiateTypeNames = new string[2]
  {
    "Mine",
    "Scene"
  };
  public GameObject Prefab;
  public int InstantiateType;
  public bool showGui;

  private void OnClick()
  {
    if (!PhotonNetwork.inRoom)
      return;
    switch (this.InstantiateType)
    {
      case 0:
        PhotonNetwork.Instantiate(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0.0f, 5f, 0.0f), Quaternion.identity, 0);
        break;
      case 1:
        PhotonNetwork.InstantiateSceneObject(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0.0f, 5f, 0.0f), Quaternion.identity, 0, (object[]) null);
        break;
    }
  }

  private void OnGUI()
  {
    if (!this.showGui)
      return;
    GUILayout.BeginArea(new Rect((float) (Screen.width - 180), 0.0f, 180f, 50f));
    this.InstantiateType = GUILayout.Toolbar(this.InstantiateType, this.InstantiateTypeNames);
    GUILayout.EndArea();
  }
}
