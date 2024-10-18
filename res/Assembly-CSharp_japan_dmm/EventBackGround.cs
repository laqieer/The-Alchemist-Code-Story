// Decompiled with JetBrains decompiler
// Type: EventBackGround
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class EventBackGround : MonoBehaviour
{
  public static List<EventBackGround> Instances = new List<EventBackGround>();
  public bool mClose;

  public static EventBackGround Find()
  {
    foreach (EventBackGround instance in EventBackGround.Instances)
    {
      if (Object.op_Inequality((Object) instance, (Object) null))
        return instance;
    }
    return (EventBackGround) null;
  }

  public static void DiscardAll()
  {
    foreach (EventBackGround instance in EventBackGround.Instances)
    {
      if (!((Component) instance).gameObject.activeInHierarchy)
        Object.Destroy((Object) ((Component) instance).gameObject);
    }
    EventBackGround.Instances.Clear();
  }

  private void Awake() => EventBackGround.Instances.Add(this);

  private void OnDestroy()
  {
    RawImage component = ((Component) this).GetComponent<RawImage>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.texture = (Texture) null;
      Object.Destroy((Object) component);
    }
    EventBackGround.Instances.Remove(this);
  }

  public void Open()
  {
    ((Component) this).gameObject.SetActive(true);
    this.mClose = false;
  }

  public void Close()
  {
    ((Component) this).gameObject.SetActive(false);
    this.mClose = true;
  }
}
