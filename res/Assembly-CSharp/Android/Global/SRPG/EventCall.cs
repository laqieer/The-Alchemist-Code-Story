// Decompiled with JetBrains decompiler
// Type: SRPG.EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
