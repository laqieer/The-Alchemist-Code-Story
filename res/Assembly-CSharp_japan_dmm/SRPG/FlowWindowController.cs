// Decompiled with JetBrains decompiler
// Type: SRPG.FlowWindowController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class FlowWindowController
  {
    private bool m_Enabled = true;
    private FlowNode m_FlowNode;
    private List<FlowWindowBase> m_List = new List<FlowWindowBase>();

    public bool enabled
    {
      set => this.m_Enabled = value;
      get => this.m_Enabled;
    }

    public void Initialize(FlowNode node) => this.m_FlowNode = node;

    public void Release()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Release();
      this.m_List.Clear();
    }

    public void Start()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Start();
    }

    public void Update()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Update(this);
    }

    public void LateUpdate()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].LateUpdate(this.m_FlowNode);
    }

    public void ActivateOutputLinks(int pinId)
    {
      if (!this.enabled || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_FlowNode, (UnityEngine.Object) null))
        return;
      this.m_FlowNode.ActivateOutputLinks(pinId);
    }

    public void Add(FlowWindowBase.SerializeParamBase param)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) param.window, (UnityEngine.Object) null) || !(Activator.CreateInstance(param.type) is FlowWindowBase instance))
        return;
      instance.Initialize(param);
      this.m_List.Add(instance);
    }

    public void Remove(FlowWindowBase window)
    {
      window.Release();
      this.m_List.Remove(window);
    }

    public FlowWindowBase GetWindow(string name)
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (this.m_List[index].name == name)
          return this.m_List[index];
      }
      return (FlowWindowBase) null;
    }

    public FlowWindowBase GetWindow(System.Type type)
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if ((object) this.m_List[index].GetType() == (object) type)
          return this.m_List[index];
      }
      return (FlowWindowBase) null;
    }

    public T GetWindow<T>() where T : FlowWindowBase
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (this.m_List[index] is T)
          return this.m_List[index] as T;
      }
      return (T) null;
    }

    public bool IsStartUp()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (!this.m_List[index].IsStartUp())
          return false;
      }
      return true;
    }

    public void OnActivate(int pinId)
    {
      if (!this.enabled)
        return;
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].RequestPin(pinId);
    }
  }
}
