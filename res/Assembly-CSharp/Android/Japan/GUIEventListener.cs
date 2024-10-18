// Decompiled with JetBrains decompiler
// Type: GUIEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GUIEventListener : MonoBehaviour
{
  public GUIEventListener.GUIEvent Listeners;

  private void OnGUI()
  {
    if (this.Listeners == null)
      return;
    this.Listeners(this.gameObject);
  }

  public delegate void GUIEvent(GameObject go);
}
