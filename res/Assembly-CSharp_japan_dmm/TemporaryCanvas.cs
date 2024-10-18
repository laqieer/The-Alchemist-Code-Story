// Decompiled with JetBrains decompiler
// Type: TemporaryCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class TemporaryCanvas : MonoBehaviour
{
  public GameObject Instance;

  private void OnApplicationQuit() => this.Instance = (GameObject) null;

  private void Update()
  {
    if (!Object.op_Equality((Object) this.Instance, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
