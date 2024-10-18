// Decompiled with JetBrains decompiler
// Type: SRPG.EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
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
