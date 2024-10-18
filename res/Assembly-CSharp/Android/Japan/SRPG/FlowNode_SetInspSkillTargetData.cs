// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetInspSkillTargetData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("InspSkill/SetTargetData", 32741)]
  [FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Done", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SetInspSkillTargetData : FlowNode
  {
    [FlowNode.ShowInInfo]
    public FlowNode_SetInspSkillTargetData.SetType setType;
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject target;

    public override void OnActivate(int pinID)
    {
      GameObject root = !((UnityEngine.Object) this.target != (UnityEngine.Object) null) ? this.gameObject : this.target;
      switch (this.setType)
      {
        case FlowNode_SetInspSkillTargetData.SetType.TargetArtifact:
          ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(root, (ArtifactData) null);
          if (dataOfClass1 != null)
          {
            GlobalVars.TargetInspSkillArtifact = dataOfClass1;
            break;
          }
          break;
        case FlowNode_SetInspSkillTargetData.SetType.TargetInspirationSkill:
          InspirationSkillData dataOfClass2 = DataSource.FindDataOfClass<InspirationSkillData>(root, (InspirationSkillData) null);
          if (dataOfClass2 != null)
          {
            GlobalVars.TargetInspSkill = dataOfClass2;
            break;
          }
          break;
        case FlowNode_SetInspSkillTargetData.SetType.MixArtifact:
          List<ArtifactData> dataOfClass3 = DataSource.FindDataOfClass<List<ArtifactData>>(root, (List<ArtifactData>) null);
          if (dataOfClass3 != null)
          {
            if (GlobalVars.MixInspSkillArtifactList == null)
              GlobalVars.MixInspSkillArtifactList = new List<ArtifactData>();
            for (int index = 0; index < dataOfClass3.Count; ++index)
              GlobalVars.MixInspSkillArtifactList.Add(dataOfClass3[index]);
            break;
          }
          break;
        case FlowNode_SetInspSkillTargetData.SetType.ClearTargetArtifact:
          GlobalVars.TargetInspSkillArtifact = (ArtifactData) null;
          break;
        case FlowNode_SetInspSkillTargetData.SetType.ClearTargetInspirationSkill:
          GlobalVars.TargetInspSkill = (InspirationSkillData) null;
          break;
        case FlowNode_SetInspSkillTargetData.SetType.ClearMixArtifact:
          GlobalVars.MixInspSkillArtifactList = (List<ArtifactData>) null;
          break;
      }
      this.ActivateOutputLinks(10);
    }

    public enum SetType
    {
      TargetArtifact,
      TargetInspirationSkill,
      MixArtifact,
      ClearTargetArtifact,
      ClearTargetInspirationSkill,
      ClearMixArtifact,
    }
  }
}
