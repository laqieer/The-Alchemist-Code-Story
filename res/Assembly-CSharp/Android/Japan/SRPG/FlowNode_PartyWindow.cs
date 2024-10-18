// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/PartyWindow")]
  public class FlowNode_PartyWindow : FlowNode_GUI
  {
    public bool ShowQuestInfo = true;
    public bool UseQuest = true;
    public PartyWindow2.EditPartyTypes PartyType;
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
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.Instance != (UnityEngine.Object) null)
      {
        PartyWindow2 component = this.Instance.GetComponent<PartyWindow2>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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
      PartyWindow2 component = pw.GetComponent<PartyWindow2>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.MainRect.gameObject.SetActive(false);
    }

    public enum TriBool
    {
      Unchanged,
      False,
      True,
    }
  }
}
