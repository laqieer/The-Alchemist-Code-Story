// Decompiled with JetBrains decompiler
// Type: OnJoinedInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OnJoinedInstantiate : MonoBehaviour
{
  public float PositionOffset = 2f;
  public Transform SpawnPosition;
  public GameObject[] PrefabsToInstantiate;

  public void OnJoinedRoom()
  {
    if (this.PrefabsToInstantiate == null)
      return;
    foreach (GameObject gameObject in this.PrefabsToInstantiate)
    {
      Debug.Log((object) ("Instantiating: " + gameObject.name));
      Vector3 vector3_1 = Vector3.up;
      if ((Object) this.SpawnPosition != (Object) null)
        vector3_1 = this.SpawnPosition.position;
      Vector3 vector3_2 = Random.insideUnitSphere;
      vector3_2.y = 0.0f;
      vector3_2 = vector3_2.normalized;
      Vector3 position = vector3_1 + this.PositionOffset * vector3_2;
      PhotonNetwork.Instantiate(gameObject.name, position, Quaternion.identity, 0);
    }
  }
}
