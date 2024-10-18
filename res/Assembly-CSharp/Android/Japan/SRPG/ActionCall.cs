﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ActionCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class ActionCall : MonoBehaviour
  {
    public string m_EventName;
    public ActionCall.CustomEvent m_Events;
    public SerializeValueList m_Value;

    private void Awake()
    {
      this.m_Value.AddField("self", this.gameObject);
      this.m_Events.Invoke(this.m_EventName, ActionCall.EventType.AWAKE, this.m_Value);
    }

    private void Start()
    {
      this.m_Events.Invoke(this.m_EventName, ActionCall.EventType.START, this.m_Value);
    }

    private void Update()
    {
      this.m_Events.Invoke(this.m_EventName, ActionCall.EventType.UPDATE, this.m_Value);
    }

    private void OnEnable()
    {
      this.m_Events.Invoke(this.m_EventName, ActionCall.EventType.ONENABLE, this.m_Value);
    }

    private void OnDisable()
    {
      this.m_Events.Invoke(this.m_EventName, ActionCall.EventType.ONDISABLE, this.m_Value);
    }

    public void Invoke(ActionCall.EventType eventType, SerializeValueList value)
    {
      SerializeValueList serializeValueList = new SerializeValueList(this.m_Value);
      serializeValueList.Write(value);
      this.m_Events.Invoke(this.m_EventName, eventType, serializeValueList);
    }

    public enum EventType
    {
      AWAKE,
      START,
      UPDATE,
      ONENABLE,
      ONDISABLE,
      OPEN,
      OPENED,
      EVERY,
      CLOSE,
      CLOSED,
      CUSTOM1,
      CUSTOM2,
      CUSTOM3,
      CUSTOM4,
      CUSTOM5,
      CUSTOM6,
      CUSTOM7,
      CUSTOM8,
    }

    [Serializable]
    public class CustomEvent : UnityEvent<string, ActionCall.EventType, SerializeValueList>
    {
    }
  }
}
