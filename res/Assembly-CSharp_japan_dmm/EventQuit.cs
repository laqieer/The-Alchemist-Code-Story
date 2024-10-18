// Decompiled with JetBrains decompiler
// Type: EventQuit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class EventQuit : MonoBehaviour
{
  private static EventQuit Instance { get; set; }

  public static EventQuit Find() => EventQuit.Instance;

  public UnityAction OnClick { private get; set; }

  public void Quit()
  {
    if (this.OnClick == null)
      return;
    this.OnClick.Invoke();
    EventScript.ActiveButtons(false);
  }

  private void Awake()
  {
    if (Object.op_Inequality((Object) null, (Object) EventQuit.Instance))
      Object.Destroy((Object) this);
    EventQuit.Instance = this;
  }

  private void OnDestroy()
  {
    if (!Object.op_Equality((Object) this, (Object) EventQuit.Instance))
      return;
    EventQuit.Instance = (EventQuit) null;
  }

  private void Update()
  {
  }
}
