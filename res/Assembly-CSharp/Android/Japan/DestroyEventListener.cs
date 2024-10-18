// Decompiled with JetBrains decompiler
// Type: DestroyEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
