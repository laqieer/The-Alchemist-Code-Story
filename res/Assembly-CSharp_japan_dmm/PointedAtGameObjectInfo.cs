// Decompiled with JetBrains decompiler
// Type: PointedAtGameObjectInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (InputToEvent))]
public class PointedAtGameObjectInfo : MonoBehaviour
{
  private void OnGUI()
  {
    if (!Object.op_Inequality((Object) InputToEvent.goPointedAt, (Object) null))
      return;
    PhotonView photonView = InputToEvent.goPointedAt.GetPhotonView();
    if (!Object.op_Inequality((Object) photonView, (Object) null))
      return;
    GUI.Label(new Rect(Input.mousePosition.x + 5f, (float) ((double) Screen.height - (double) Input.mousePosition.y - 15.0), 300f, 30f), string.Format("ViewID {0} {1}{2}", (object) photonView.viewID, !photonView.isSceneView ? (object) string.Empty : (object) "scene ", !photonView.isMine ? (object) ("owner: " + (object) photonView.ownerId) : (object) "mine"));
  }
}
