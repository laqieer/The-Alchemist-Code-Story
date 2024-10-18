// Decompiled with JetBrains decompiler
// Type: EventQuit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

public class EventQuit : MonoBehaviour
{
  private static EventQuit Instance { get; set; }

  public static EventQuit Find()
  {
    return EventQuit.Instance;
  }

  public UnityAction OnClick { private get; set; }

  public void Quit()
  {
    if (this.OnClick == null)
      return;
    this.OnClick();
  }

  private void Awake()
  {
    if ((Object) null != (Object) EventQuit.Instance)
      Object.Destroy((Object) this);
    EventQuit.Instance = this;
  }

  private void OnDestroy()
  {
    if (!((Object) this == (Object) EventQuit.Instance))
      return;
    EventQuit.Instance = (EventQuit) null;
  }

  private void Update()
  {
  }
}
