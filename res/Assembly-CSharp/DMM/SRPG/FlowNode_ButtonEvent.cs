// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/ButtonEvent", 15695845)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_ButtonEvent : FlowNode
  {
    public static object currentValue;
    [StringIsButtonEventID]
    public string EventName;
    [BitMask]
    public CriticalSections CSMask = (CriticalSections) -1;
    public bool DoLock;
    public string LockKey;
    private ButtonEvent.Listener m_Listener;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[2]
      {
        "Event is " + this.EventName,
        "CsMask is " + (object) this.CSMask
      };
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
      if (!isForce && ((CriticalSection.GetActive() & this.CSMask) != (CriticalSections) 0 || UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null) && HomeWindow.Current.IsSceneChanging || SRPG_InputField.IsFocus))
        return;
      if (this.DoLock)
        ButtonEvent.Lock(this.LockKey);
      FlowNode_ButtonEvent.currentValue = obj;
      this.ActivateOutputLinks(1);
    }
  }
}
