// Decompiled with JetBrains decompiler
// Type: OnClickInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class OnClickInstantiate : MonoBehaviour
{
  public GameObject Prefab;
  public int InstantiateType;
  private string[] InstantiateTypeNames = new string[2]
  {
    "Mine",
    "Scene"
  };
  public bool showGui;

  private void OnClick()
  {
    if (!PhotonNetwork.inRoom)
      return;
    switch (this.InstantiateType)
    {
      case 0:
        PhotonNetwork.Instantiate(((Object) this.Prefab).name, Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.identity, (byte) 0);
        break;
      case 1:
        PhotonNetwork.InstantiateSceneObject(((Object) this.Prefab).name, Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.identity, (byte) 0, (object[]) null);
        break;
    }
  }

  private void OnGUI()
  {
    if (!this.showGui)
      return;
    GUILayout.BeginArea(new Rect((float) (Screen.width - 180), 0.0f, 180f, 50f));
    this.InstantiateType = GUILayout.Toolbar(this.InstantiateType, this.InstantiateTypeNames, new GUILayoutOption[0]);
    GUILayout.EndArea();
  }
}
