// Decompiled with JetBrains decompiler
// Type: TemporaryCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class TemporaryCanvas : MonoBehaviour
{
  public GameObject Instance;

  private void OnApplicationQuit()
  {
    this.Instance = (GameObject) null;
  }

  private void Update()
  {
    if (!((Object) this.Instance == (Object) null))
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
