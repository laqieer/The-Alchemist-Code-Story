// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/PartyWindow")]
  public class FlowNode_PartyWindow : FlowNode_GUI
  {
    public PartyWindow2.EditPartyTypes PartyType;
    public bool ShowQuestInfo = true;
    public bool UseQuest = true;
    public bool ForceRefresh;
    public FlowNode_PartyWindow.TriBool BackButton;
    public FlowNode_PartyWindow.TriBool ForwardButton;
    public FlowNode_PartyWindow.TriBool ShowRaidInfo;
    public FlowNode_PartyWindow.TriBool EnableTeamAssign;
    public FlowNode_PartyWindow.TriBool IsShowDownloadPopup;

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      PartyWindow2 componentInChildren = this.Instance.GetComponentInChildren<PartyWindow2>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.PartyType = this.PartyType;
      componentInChildren.ShowQuestInfo = this.ShowQuestInfo;
      componentInChildren.UseQuestInfo = this.UseQuest;
      if (this.BackButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowBackButton = this.BackButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ForwardButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowForwardButton = this.ForwardButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ShowRaidInfo != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowRaidInfo = this.ShowRaidInfo == FlowNode_PartyWindow.TriBool.True;
      if (this.EnableTeamAssign != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.EnableTeamAssign = this.EnableTeamAssign == FlowNode_PartyWindow.TriBool.True;
      if (this.IsShowDownloadPopup != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.SetIsShowDownloadPopup(this.IsShowDownloadPopup == FlowNode_PartyWindow.TriBool.True);
      this.OffCanvas(componentInChildren);
    }

    protected override void OnCreatePinActive()
    {
      if (Object.op_Inequality((Object) this.Instance, (Object) null))
      {
        PartyWindow2 component = this.Instance.GetComponent<PartyWindow2>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        this.OffCanvas(component);
        component.Reopen(this.ForceRefresh);
      }
      else
        base.OnCreatePinActive();
    }

    private void OffCanvas(PartyWindow2 pw)
    {
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        return;
      PartyWindow2 component = ((Component) pw).GetComponent<PartyWindow2>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Component) component.MainRect).gameObject.SetActive(false);
    }

    public enum TriBool
    {
      Unchanged,
      False,
      True,
    }
  }
}
