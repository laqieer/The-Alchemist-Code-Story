// Decompiled with JetBrains decompiler
// Type: OnClickInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OnClickInstantiate : MonoBehaviour
{
  private string[] InstantiateTypeNames = new string[2]{ "Mine", "Scene" };
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
