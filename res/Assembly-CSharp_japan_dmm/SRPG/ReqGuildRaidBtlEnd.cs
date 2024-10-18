// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidBtlEnd : WebAPI
  {
    public ReqGuildRaidBtlEnd(
      int gid,
      long btlid,
      BtlResultTypes result,
      int bossId,
      int turn,
      List<Unit> enemyUnits,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BattleCore.Record record = null,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "guildraid/btl/end";
      ReqGuildRaidBtlEnd.RequestParam requestParam = new ReqGuildRaidBtlEnd.RequestParam();
      requestParam.gid = gid;
      requestParam.btlid = btlid;
      switch (result)
      {
        case BtlResultTypes.Win:
          requestParam.status = "win";
          break;
        case BtlResultTypes.Lose:
          requestParam.status = "lose";
          break;
        case BtlResultTypes.Retire:
          requestParam.status = "retire";
          break;
        case BtlResultTypes.Cancel:
          requestParam.status = "cancel";
          break;
      }
      requestParam.boss_info = new JSON_GuildRaidBossInfo();
      requestParam.boss_info.boss_id = bossId;
      if (MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(bossId) != null && enemyUnits != null)
      {
        Unit unit = enemyUnits.Find((Predicate<Unit>) (enemy => enemy.SettingNPC != null && (bool) enemy.SettingNPC.is_raid_boss));
        requestParam.boss_info.current_hp = (int) unit.CurrentStatus.param.hp - unit.OverKillDamage;
      }
      requestParam.btlendparam = new ReqGuildRaidBtlEnd.RequestEnd();
      if (usedItems != null)
      {
        requestParam.btlendparam.inputs = new ReqGuildRaidBtlEnd.RequestEndInput[usedItems.Count];
        int index = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in usedItems)
        {
          requestParam.btlendparam.inputs[index] = new ReqGuildRaidBtlEnd.RequestEndInput();
          requestParam.btlendparam.inputs[index].use = (string) usedItem.Key;
          requestParam.btlendparam.inputs[index].n = (int) usedItem.Value;
          ++index;
        }
      }
      requestParam.btlendparam.auto_status = record == null || !record.mIsUseAutoPlayMode ? (record == null || record.mIsUseAutoPlayMode ? "none" : "off") : "on";
      requestParam.btlendparam.turn = turn;
      this.body = WebAPI.GetRequestString<ReqGuildRaidBtlEnd.RequestParam>(requestParam);
      if (!string.IsNullOrEmpty(trophyprog) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), trophyprog + ",");
      if (!string.IsNullOrEmpty(bingoprog) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), bingoprog + ",");
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long btlid;
      public string status;
      public JSON_GuildRaidBossInfo boss_info;
      public ReqGuildRaidBtlEnd.RequestEnd btlendparam;
      public int gid;
    }

    [Serializable]
    public class RequestEnd
    {
      public ReqGuildRaidBtlEnd.RequestEndInput[] inputs;
      public string auto_status;
      public int turn;
    }

    [Serializable]
    public class RequestEndInput
    {
      public string use;
      public int n;
    }

    [Serializable]
    public class Response : Json_PlayerDataAll
    {
      public int total_damage;
      public long total_score;
      public JSON_GuildRaidBossInfo boss_info;
    }
  }
}
