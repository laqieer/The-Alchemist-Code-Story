// Decompiled with JetBrains decompiler
// Type: PointedAtGameObjectInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (InputToEvent))]
public class PointedAtGameObjectInfo : MonoBehaviour
{
  private void OnGUI()
  {
    if (!((Object) InputToEvent.goPointedAt != (Object) null))
      return;
    PhotonView photonView = InputToEvent.goPointedAt.GetPhotonView();
    if (!((Object) photonView != (Object) null))
      return;
    GUI.Label(new Rect(Input.mousePosition.x + 5f, (float) ((double) Screen.height - (double) Input.mousePosition.y - 15.0), 300f, 30f), string.Format("ViewID {0} {1}{2}", (object) photonView.viewID, !photonView.isSceneView ? (object) string.Empty : (object) "scene ", !photonView.isMine ? (object) ("owner: " + (object) photonView.ownerId) : (object) "mine"));
  }
}
