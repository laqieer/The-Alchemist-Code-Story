// Decompiled with JetBrains decompiler
// Type: DestroyEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DestroyEventListener : MonoBehaviour
{
  public DestroyEventListener.DestroyEvent Listeners = (DestroyEventListener.DestroyEvent) (_param0 => {});

  private void OnApplicationQuit()
  {
    this.Listeners = (DestroyEventListener.DestroyEvent) (_param0 => {});
  }

  private void OnDestroy()
  {
    if (this.Listeners == null)
      return;
    this.Listeners(this.gameObject);
  }

  public delegate void DestroyEvent(GameObject go);
}
