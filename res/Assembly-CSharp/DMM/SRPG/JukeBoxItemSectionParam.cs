// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxItemSectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  public class JukeBoxItemSectionParam : ContentSource.Param
  {
    public JukeBoxSectionParam SectionParam;
    public UnityAction OnClickAction;
    private List<JukeBoxItemSectionNode> mNodeList = new List<JukeBoxItemSectionNode>();
    private bool mIsCurrent;
    private bool mNewFlag;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNodeList.Add(node as JukeBoxItemSectionNode);
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      base.OnDisable(node);
      this.mNodeList.Remove(node as JukeBoxItemSectionNode);
      if (this.mNodeList.Count > 0)
        return;
      this.mNodeList.Clear();
    }

    public void Refresh()
    {
      if (this.mNodeList.Count <= 0 || this.SectionParam == null)
        return;
      foreach (JukeBoxItemSectionNode mNode in this.mNodeList)
        mNode.Setup(this, this.mIsCurrent, this.mNewFlag, this.OnClickAction);
    }

    public void SetCurrent(bool is_active)
    {
      this.mIsCurrent = is_active;
      if (this.mNodeList.Count <= 0)
        return;
      foreach (JukeBoxItemSectionNode mNode in this.mNodeList)
        mNode.SetCurrent(is_active);
    }

    public bool IsCurrent() => this.mIsCurrent;

    public void SetNewBadge(bool is_new)
    {
      this.mNewFlag = is_new;
      if (this.mNodeList.Count <= 0)
        return;
      foreach (JukeBoxItemSectionNode mNode in this.mNodeList)
        mNode.SetNewBadge(is_new);
    }
  }
}
