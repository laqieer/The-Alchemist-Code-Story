// Decompiled with JetBrains decompiler
// Type: SRPG.GvGScoreRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GvGScoreRankingData
  {
    public int Rank { get; private set; }

    public int Score { get; private set; }

    public string Name { get; private set; }

    public int Level { get; private set; }

    public UnitData Unit { get; private set; }

    public int Role { get; private set; }

    public ViewGuildData Guild { get; private set; }

    public bool Deserialize(JSON_GvGScoreRanking json)
    {
      if (json == null)
        return false;
      this.Rank = json.rank;
      this.Score = json.score;
      this.Name = json.name;
      this.Level = json.level;
      this.Unit = UnitData.CreateUnitDataForDisplay(MonoSingleton<GameManager>.Instance.GetUnitParam(json.unit));
      if (this.Unit != null)
        this.Unit.SetJobSkinAll(json.skin, false);
      this.Role = json.role;
      GvGManager instance = GvGManager.Instance;
      this.Guild = (ViewGuildData) null;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      if (instance.MyGuild.id == json.gid)
      {
        this.Guild = (ViewGuildData) instance.MyGuild;
      }
      else
      {
        for (int index = 0; index < instance.OtherGuildList.Count; ++index)
        {
          if (instance.OtherGuildList[index].id == json.gid)
          {
            this.Guild = (ViewGuildData) instance.OtherGuildList[index];
            break;
          }
        }
      }
      return true;
    }
  }
}
