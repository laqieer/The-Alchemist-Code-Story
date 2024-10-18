// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  public class JukeBoxItemParam : ContentSource.Param
  {
    public JukeBoxWindow.JukeBoxData ItemData;
    public UnityAction OnClickAction;
    public UnityAction OnClickActionLock;
    public UnityAction LongTapAction;
    private JukeBoxItemNode mNode;
    private bool mIsCurrent;
    private bool mNewFlag;
    private bool mMylistFlag;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as JukeBoxItemNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      base.OnDisable(node);
      this.mNode = (JukeBoxItemNode) null;
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null) || this.ItemData == null)
        return;
      this.mNode.Setup(this, this.mIsCurrent, this.mNewFlag, this.mMylistFlag, this.OnClickAction, this.OnClickActionLock);
    }

    public void SetCurrent(bool is_active)
    {
      this.mIsCurrent = is_active;
      if (!Object.op_Implicit((Object) this.mNode))
        return;
      this.mNode.SetCurrent(is_active);
    }

    public bool IsCurrent() => this.mIsCurrent;

    public void SetNewBadge(bool is_new)
    {
      this.mNewFlag = is_new;
      if (!Object.op_Implicit((Object) this.mNode))
        return;
      this.mNode.SetNewBadge(is_new);
    }

    public bool IsNewBadge() => this.mNewFlag;

    public void SetMylistFlag(bool is_mylist)
    {
      this.mMylistFlag = is_mylist;
      if (!Object.op_Implicit((Object) this.mNode))
        return;
      this.mNode.SetMylist(is_mylist);
    }
  }
}
