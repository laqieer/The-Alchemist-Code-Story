// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRankingParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WorldRaidRankingParam : ContentSource.Param
  {
    public WorldRaidRankingData WorldRaidRanking;
    private WorldRaidRankingContentNode mNode;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as WorldRaidRankingContentNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      this.mNode = (WorldRaidRankingContentNode) null;
      base.OnDisable(node);
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null))
        return;
      this.mNode.Setup(this.WorldRaidRanking);
    }
  }
}
