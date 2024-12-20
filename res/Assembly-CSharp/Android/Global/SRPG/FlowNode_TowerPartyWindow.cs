﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TowerPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/TowerPartyWindow")]
  public class FlowNode_TowerPartyWindow : FlowNode_GUI
  {
    public bool ShowQuestInfo = true;
    public PartyWindow2.EditPartyTypes PartyType;
    public FlowNode_TowerPartyWindow.TriBool BackButton;
    public FlowNode_TowerPartyWindow.TriBool ForwardButton;
    public FlowNode_TowerPartyWindow.TriBool ShowRaidInfo;

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      TowerPartyWindow componentInChildren = this.Instance.GetComponentInChildren<TowerPartyWindow>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
        return;
      componentInChildren.PartyType = this.PartyType;
      componentInChildren.ShowQuestInfo = this.ShowQuestInfo;
      if (this.BackButton != FlowNode_TowerPartyWindow.TriBool.Unchanged)
        componentInChildren.ShowBackButton = this.BackButton == FlowNode_TowerPartyWindow.TriBool.True;
      if (this.ForwardButton != FlowNode_TowerPartyWindow.TriBool.Unchanged)
        componentInChildren.ShowForwardButton = this.ForwardButton == FlowNode_TowerPartyWindow.TriBool.True;
      if (this.ShowRaidInfo == FlowNode_TowerPartyWindow.TriBool.Unchanged)
        return;
      componentInChildren.ShowRaidInfo = this.ShowRaidInfo == FlowNode_TowerPartyWindow.TriBool.True;
    }

    protected override void OnCreatePinActive()
    {
      if ((UnityEngine.Object) this.Instance != (UnityEngine.Object) null)
      {
        TowerPartyWindow component = this.Instance.GetComponent<TowerPartyWindow>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.Reopen();
        GameParameter.UpdateAll(component.gameObject);
      }
      else
        base.OnCreatePinActive();
    }

    public enum TriBool
    {
      Unchanged,
      False,
      True,
    }
  }
}
