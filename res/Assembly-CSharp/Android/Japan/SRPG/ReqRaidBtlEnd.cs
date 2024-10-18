// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ReqRaidBtlEnd : WebAPI
  {
    public ReqRaidBtlEnd(long btlid, BtlResultTypes result, int bossId, List<Unit> enemyUnits, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response)
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
        requestParam.boss_info.current_hp = (int) unit.CurrentStatus.param.hp - (int) unit.OverKillDamage;
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
      }
      this.body = WebAPI.GetRequestString<ReqRaidBtlEnd.RequestParam>(requestParam);
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
      public int is_timeover;
    }
  }
}
