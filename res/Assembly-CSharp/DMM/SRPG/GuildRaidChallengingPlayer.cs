// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidChallengingPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildRaidChallengingPlayer
  {
    public string Name { get; private set; }

    public UnitParam Unit { get; private set; }

    public int Lv { get; private set; }

    public AwardParam SelectedAward { get; private set; }

    public GuildMemberData.eRole Role { get; private set; }

    public DateTime ChallengeTime { get; private set; }

    public bool Deserialize(JSON_GuildRaidChallengingPlayer json)
    {
      if (json == null)
        return false;
      this.Name = json.name;
      try
      {
        this.Unit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.unit);
      }
      catch (Exception ex)
      {
        Debug.Log((object) ex);
        return false;
      }
      this.Lv = json.lv;
      if (!string.IsNullOrEmpty(json.selected_award))
        this.SelectedAward = MonoSingleton<GameManager>.Instance.GetAwardParam(json.selected_award);
      this.Role = (GuildMemberData.eRole) json.role_id;
      this.ChallengeTime = TimeManager.FromUnixTime((long) json.challenge_time);
      return true;
    }
  }
}
