// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingGuildParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidRankingGuildParam : ContentSource.Param
  {
    public GuildRaidRanking mGuildRaidRanking;
    public bool IsEmpty;
    private GuildRaidRankingGuildNode mNode;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as GuildRaidRankingGuildNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      this.mNode = (GuildRaidRankingGuildNode) null;
      base.OnDisable(node);
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null))
        return;
      this.mNode.Empty(this.IsEmpty);
      this.mNode.Setup(this.mGuildRaidRanking);
    }
  }
}
