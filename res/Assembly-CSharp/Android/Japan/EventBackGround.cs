// Decompiled with JetBrains decompiler
// Type: EventBackGround
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class EventBackGround : MonoBehaviour
{
  public static List<EventBackGround> Instances = new List<EventBackGround>();
  public bool mClose;

  public static EventBackGround Find()
  {
    foreach (EventBackGround instance in EventBackGround.Instances)
    {
      if ((Object) instance != (Object) null)
        return instance;
    }
    return (EventBackGround) null;
  }

  public static void DiscardAll()
  {
    foreach (EventBackGround instance in EventBackGround.Instances)
    {
      if (!instance.gameObject.activeInHierarchy)
        Object.Destroy((Object) instance.gameObject);
    }
    EventBackGround.Instances.Clear();
  }

  private void Awake()
  {
    EventBackGround.Instances.Add(this);
  }

  private void OnDestroy()
  {
    EventBackGround.Instances.Remove(this);
  }

  public void Open()
  {
    this.gameObject.SetActive(true);
    this.mClose = false;
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
    this.mClose = true;
  }
}
