// Decompiled with JetBrains decompiler
// Type: SRPG.ReqWorldRaidBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqWorldRaidBtlEnd : WebAPI
  {
    public ReqWorldRaidBtlEnd(
      long btlid,
      BtlResultTypes result,
      string bossId,
      int[] damages,
      List<Unit> enemyUnits,
      Dictionary<OString, OInt> usedItems,
      Network.ResponseCallback response,
      BattleCore.Record record = null,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "worldraid/btl/end";
      ReqWorldRaidBtlEnd.RequestParam requestParam = new ReqWorldRaidBtlEnd.RequestParam()
      {
        btlid = btlid
      };
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
      if (WorldRaidBossParam.GetParam(bossId) != null && enemyUnits != null)
      {
        Unit unit = enemyUnits.Find((Predicate<Unit>) (enemy => enemy.SettingNPC != null && (bool) enemy.SettingNPC.is_raid_boss));
        long num = unit.BossLongCurHP - (long) unit.OverKillDamage;
        requestParam.boss_info = new JSON_WorldRaidBossData()
        {
          boss_iname = bossId,
          current_hp = num
        };
      }
      requestParam.btlendparam = new ReqWorldRaidBtlEnd.RequestEnd();
      if (usedItems != null)
      {
        requestParam.btlendparam.inputs = new ReqWorldRaidBtlEnd.RequestEndInput[usedItems.Count];
        int index = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in usedItems)
        {
          requestParam.btlendparam.inputs[index] = new ReqWorldRaidBtlEnd.RequestEndInput();
          requestParam.btlendparam.inputs[index].use = (string) usedItem.Key;
          requestParam.btlendparam.inputs[index].n = (int) usedItem.Value;
          ++index;
        }
      }
      requestParam.btlendparam.auto_status = record == null || !record.mIsUseAutoPlayMode ? (record == null || record.mIsUseAutoPlayMode ? "none" : "off") : "on";
      requestParam.btlendparam.damages = damages;
      this.body = WebAPI.GetRequestString<ReqWorldRaidBtlEnd.RequestParam>(requestParam);
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
      public JSON_WorldRaidBossData boss_info;
      public ReqWorldRaidBtlEnd.RequestEnd btlendparam;
    }

    [Serializable]
    public class RequestEnd
    {
      public ReqWorldRaidBtlEnd.RequestEndInput[] inputs;
      public string auto_status;
      public int[] damages;
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
      public Json_RuneData[] runes_detail;
      public int is_mail_cards;
      public Json_BtlRewardConceptCard[] cards;
      public JSON_WorldRaidBossData boss_info;
      public ReqWorldRaidBtlEnd.ResponseReward reward;
      public long total_damage;
    }

    [Serializable]
    public class ResponseReward
    {
      public JSON_WorldRaidBattleRewardData[] drop_lottery;
      public JSON_WorldRaidBattleRewardData[] last_attack;
    }
  }
}
