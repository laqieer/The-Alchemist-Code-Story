﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckArtifactStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tips/CheckArtifactStatus", 32741)]
  [FlowNode.Pin(1, "判定", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "True", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "False", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_CheckArtifactStatus : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_TRUE = 2;
    private const int PIN_ID_FALSE = 3;
    [SerializeField]
    private FlowNode_CheckArtifactStatus.Flag flag;

    private ArtifactData GetArtifactDataFromUniqueID(long uniqueId)
    {
      return MonoSingleton<GameManager>.Instance.Player.Artifacts.FirstOrDefault<ArtifactData>((Func<ArtifactData, bool>) (arti => (long) arti.UniqueID == uniqueId));
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      switch (this.flag)
      {
        case FlowNode_CheckArtifactStatus.Flag.ArmorCountLessThan3:
          if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Count<ArtifactData>((Func<ArtifactData, bool>) (arti => arti.ArtifactParam.iname == "AF_ARMO_ARMOR_BEGINNER_01")) < 3)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          this.ActivateOutputLinks(3);
          break;
        case FlowNode_CheckArtifactStatus.Flag.SelectArmor:
          ArtifactData dataFromUniqueId1 = this.GetArtifactDataFromUniqueID((long) GlobalVars.SelectedArtifactUniqueID);
          if (dataFromUniqueId1 != null && dataFromUniqueId1.ArtifactParam.iname == "AF_ARMO_ARMOR_BEGINNER_01")
          {
            this.ActivateOutputLinks(2);
            break;
          }
          this.ActivateOutputLinks(3);
          break;
        case FlowNode_CheckArtifactStatus.Flag.ArmorRarityReachedBy4:
          ArtifactData dataFromUniqueId2 = this.GetArtifactDataFromUniqueID((long) GlobalVars.SelectedArtifactUniqueID);
          if (dataFromUniqueId2 != null && (int) dataFromUniqueId2.Rarity == 3)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          this.ActivateOutputLinks(3);
          break;
      }
    }

    public enum Flag
    {
      ArmorCountLessThan3,
      SelectArmor,
      ArmorRarityReachedBy4,
    }
  }
}
