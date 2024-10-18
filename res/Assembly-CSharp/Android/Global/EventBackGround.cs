// Decompiled with JetBrains decompiler
// Type: EventBackGround
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class EventBackGround : MonoBehaviour
{
  public static List<EventBackGround> Instances = new List<EventBackGround>();
  public bool mClose;

  public static EventBackGround Find()
  {
    using (List<EventBackGround>.Enumerator enumerator = EventBackGround.Instances.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        EventBackGround current = enumerator.Current;
        if ((Object) current != (Object) null)
          return current;
      }
    }
    return (EventBackGround) null;
  }

  public static void DiscardAll()
  {
    using (List<EventBackGround>.Enumerator enumerator = EventBackGround.Instances.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        EventBackGround current = enumerator.Current;
        if (!current.gameObject.activeInHierarchy)
          Object.Destroy((Object) current.gameObject);
      }
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
