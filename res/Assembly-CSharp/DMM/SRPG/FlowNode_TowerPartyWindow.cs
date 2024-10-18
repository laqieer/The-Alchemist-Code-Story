// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TowerPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/TowerPartyWindow")]
  public class FlowNode_TowerPartyWindow : FlowNode_GUI
  {
    public PartyWindow2.EditPartyTypes PartyType;
    public bool ShowQuestInfo = true;
    public FlowNode_TowerPartyWindow.TriBool BackButton;
    public FlowNode_TowerPartyWindow.TriBool ForwardButton;
    public FlowNode_TowerPartyWindow.TriBool ShowRaidInfo;

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      TowerPartyWindow componentInChildren = this.Instance.GetComponentInChildren<TowerPartyWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
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
      if (Object.op_Inequality((Object) this.Instance, (Object) null))
      {
        TowerPartyWindow component = this.Instance.GetComponent<TowerPartyWindow>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Reopen();
        GameParameter.UpdateAll(((Component) component).gameObject);
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
