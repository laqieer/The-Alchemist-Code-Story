// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqRaidBtlEnd : WebAPI
  {
    public ReqRaidBtlEnd(
      long btlid,
      BtlResultTypes result,
      int bossId,
      List<Unit> enemyUnits,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BattleCore.Record record = null,
      string trophyProgs = "",
      string bingoProgs = "")
    {
      this.name = "raidboss/btl/end";
      ReqRaidBtlEnd.RequestParam requestParam = new ReqRaidBtlEnd.RequestParam();
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
      requestParam.boss_info = new JSON_RaidBossInfo();
      requestParam.boss_info.boss_id = bossId;
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(bossId) != null && enemyUnits != null)
      {
        Unit unit = enemyUnits.Find((Predicate<Unit>) (enemy => enemy.SettingNPC != null && (bool) enemy.SettingNPC.is_raid_boss));
        requestParam.boss_info.current_hp = (int) unit.CurrentStatus.param.hp - unit.OverKillDamage;
      }
      if (usedItems != null)
      {
        requestParam.btlendparam = new ReqRaidBtlEnd.RequestEnd();
        requestParam.btlendparam.inputs = new ReqRaidBtlEnd.RequestEndInput[usedItems.Count];
        int index = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in usedItems)
        {
          requestParam.btlendparam.inputs[index] = new ReqRaidBtlEnd.RequestEndInput();
          requestParam.btlendparam.inputs[index].use = (string) usedItem.Key;
          requestParam.btlendparam.inputs[index].n = (int) usedItem.Value;
          ++index;
        }
        requestParam.btlendparam.auto_status = record == null || !record.mIsUseAutoPlayMode ? (record == null || record.mIsUseAutoPlayMode ? "none" : "off") : "on";
      }
      this.body = WebAPI.GetRequestString<ReqRaidBtlEnd.RequestParam>(requestParam);
      if (!string.IsNullOrEmpty(trophyProgs) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), trophyProgs + ",");
      if (!string.IsNullOrEmpty(bingoProgs) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), bingoProgs + ",");
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long btlid;
      public string status;
      public JSON_RaidBossInfo boss_info;
      public ReqRaidBtlEnd.RequestEnd btlendparam;
    }

    [Serializable]
    public class RequestEnd
    {
      public ReqRaidBtlEnd.RequestEndInput[] inputs;
      public string auto_status;
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
      public Json_Gift[] raid_battle_reward;
      public int total_damage;
      public string[] damage_ratio_reward_ids;
      public string[] damage_amount_reward_ids;
      public int is_timeover;
    }
  }
}
