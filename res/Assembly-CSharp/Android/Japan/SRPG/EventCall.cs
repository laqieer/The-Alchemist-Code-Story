// Decompiled with JetBrains decompiler
// Type: SRPG.EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class EventCall : MonoBehaviour
  {
    public EventCall.CustomEvent m_Events;
    public static object currentValue;

    public void Invoke(string key, string value)
    {
      EventCall.currentValue = (object) null;
      this.m_Events.Invoke(key, value);
    }

    public void Invoke(string key, string value, object obj)
    {
      EventCall.currentValue = obj;
      this.m_Events.Invoke(key, value);
    }

    [Serializable]
    public class CustomEvent : UnityEvent<string, string>
    {
    }
  }
}
