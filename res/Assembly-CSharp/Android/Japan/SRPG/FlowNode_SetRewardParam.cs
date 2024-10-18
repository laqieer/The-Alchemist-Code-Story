﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Reward/Set", 32741)]
  [FlowNode.Pin(1, "Assign", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(9, "Assigned", FlowNode.PinTypes.Output, 9)]
  public class FlowNode_SetRewardParam : FlowNode
  {
    public GameObject target;
    public FlowNode_SetRewardParam.Type type;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if ((UnityEngine.Object) this.target == (UnityEngine.Object) null)
        this.target = this.gameObject;
      switch (this.type)
      {
        case FlowNode_SetRewardParam.Type.Item:
          GlobalVars.SelectedItemID = DataSource.FindDataOfClass<ItemParam>(this.target, (ItemParam) null).iname;
          break;
        case FlowNode_SetRewardParam.Type.Artifact:
          GlobalVars.SelectedArtifactID = DataSource.FindDataOfClass<ArtifactParam>(this.target, (ArtifactParam) null).iname;
          break;
        case FlowNode_SetRewardParam.Type.Award:
          FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", DataSource.FindDataOfClass<AwardParam>(this.target, (AwardParam) null).iname);
          break;
      }
      this.ActivateOutputLinks(9);
    }

    public enum Type
    {
      Item,
      Unit,
      Artifact,
      Award,
    }
  }
}
