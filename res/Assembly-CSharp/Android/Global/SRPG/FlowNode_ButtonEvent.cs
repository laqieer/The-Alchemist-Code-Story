﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Event/ButtonEvent", 15695845)]
  public class FlowNode_ButtonEvent : FlowNode
  {
    [BitMask]
    public CriticalSections CSMask = (CriticalSections) -1;
    public static object currentValue;
    [StringIsButtonEventID]
    public string EventName;
    public bool DoLock;
    public string LockKey;
    private ButtonEvent.Listener m_Listener;

    public override void OnActivate(int pinID)
    {
    }

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[2]{ "Event is " + this.EventName, "CsMask is " + (object) this.CSMask };
    }

    protected override void Awake()
    {
      base.Awake();
      if (string.IsNullOrEmpty(this.EventName))
        return;
      this.m_Listener = ButtonEvent.AddListener(this.EventName, new Action<bool, object>(this.OnButtonEvent));
    }

    protected override void OnDestroy()
    {
      if (this.m_Listener == null)
        return;
      ButtonEvent.RemoveListener(this.m_Listener);
      this.m_Listener = (ButtonEvent.Listener) null;
    }

    private void OnButtonEvent(bool isForce, object obj)
    {
      if (!isForce && ((CriticalSection.GetActive() & this.CSMask) != (CriticalSections) 0 || (UnityEngine.Object) HomeWindow.Current != (UnityEngine.Object) null && HomeWindow.Current.IsSceneChanging))
        return;
      if (this.DoLock)
        ButtonEvent.Lock(this.LockKey);
      FlowNode_ButtonEvent.currentValue = obj;
      this.ActivateOutputLinks(1);
    }
  }
}
