// Decompiled with JetBrains decompiler
// Type: GUIEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class GUIEventListener : MonoBehaviour
{
  public GUIEventListener.GUIEvent Listeners;

  private void OnGUI()
  {
    if (this.Listeners == null)
      return;
    this.Listeners(((Component) this).gameObject);
  }

  public delegate void GUIEvent(GameObject go);
}
