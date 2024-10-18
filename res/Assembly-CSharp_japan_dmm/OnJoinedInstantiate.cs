// Decompiled with JetBrains decompiler
// Type: OnJoinedInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class OnJoinedInstantiate : MonoBehaviour
{
  public Transform SpawnPosition;
  public float PositionOffset = 2f;
  public GameObject[] PrefabsToInstantiate;

  public void OnJoinedRoom()
  {
    if (this.PrefabsToInstantiate == null)
      return;
    foreach (GameObject gameObject in this.PrefabsToInstantiate)
    {
      Debug.Log((object) ("Instantiating: " + ((Object) gameObject).name));
      Vector3 vector3_1 = Vector3.up;
      if (Object.op_Inequality((Object) this.SpawnPosition, (Object) null))
        vector3_1 = this.SpawnPosition.position;
      Vector3 vector3_2 = Random.insideUnitSphere;
      vector3_2.y = 0.0f;
      vector3_2 = ((Vector3) ref vector3_2).normalized;
      Vector3 position = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(this.PositionOffset, vector3_2));
      PhotonNetwork.Instantiate(((Object) gameObject).name, position, Quaternion.identity, (byte) 0);
    }
  }
}
