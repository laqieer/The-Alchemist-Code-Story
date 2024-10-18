// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingGuildNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidRankingGuildNode : ContentNode
  {
    [SerializeField]
    public GameObject bindObject;
    [SerializeField]
    public GameObject EmptyObject;

    public void Setup(GuildRaidRanking guildRaidRanking)
    {
      if (Object.op_Equality((Object) this.bindObject, (Object) null) || guildRaidRanking == null)
        return;
      DataSource.Bind<GuildRaidRanking>(this.bindObject, guildRaidRanking);
      this.bindObject.gameObject.SetActive(true);
      GameParameter.UpdateAll(this.bindObject.gameObject);
    }

    public void Empty(bool is_enmpty)
    {
      if (Object.op_Equality((Object) this.EmptyObject, (Object) null) || Object.op_Equality((Object) this.bindObject, (Object) null))
        return;
      this.bindObject.gameObject.SetActive(!is_enmpty);
      this.EmptyObject.SetActive(is_enmpty);
    }
  }
}
