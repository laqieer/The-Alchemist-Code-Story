// Decompiled with JetBrains decompiler
// Type: GUIEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
