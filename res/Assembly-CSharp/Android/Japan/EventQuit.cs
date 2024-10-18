// Decompiled with JetBrains decompiler
// Type: EventQuit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
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
    EventScript.ActiveButtons(false);
  }

  private void Awake()
  {
    if ((UnityEngine.Object) null != (UnityEngine.Object) EventQuit.Instance)
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    EventQuit.Instance = this;
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) this == (UnityEngine.Object) EventQuit.Instance))
      return;
    EventQuit.Instance = (EventQuit) null;
  }

  private void Update()
  {
  }
}
