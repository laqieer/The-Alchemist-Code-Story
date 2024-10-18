// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisBossBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class ReqGenesisBossBtlEnd : WebAPI
  {
    public ReqGenesisBossBtlEnd(
      long btlid,
      BtlResultTypes result,
      List<Unit> all_units,
      int start_index,
      int[] missions,
      int[] missions_log,
      Dictionary<OString, OInt> used_items,
      Network.ResponseCallback response,
      BattleCore.Record record = null,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "genesis/raidboss/btl/end";
      ReqGenesisBossBtlEnd.RequestParam requestParam = new ReqGenesisBossBtlEnd.RequestParam();
      requestParam.btlid = btlid;
      requestParam.status = WebAPI.ConvBtlResultTypesToStatus(result);
      requestParam.btlendparam = new ReqGenesisBossBtlEnd.RequestEnd();
      if (all_units != null)
      {
        List<BattleCore.Json_GenesisEnemy> source = new List<BattleCore.Json_GenesisEnemy>(all_units.Count);
        if (all_units != null)
        {
          for (int index = start_index; index < all_units.Count; ++index)
          {
            Unit allUnit = all_units[index];
            BattleCore.Json_GenesisEnemy jsonGenesisEnemy = new BattleCore.Json_GenesisEnemy();
            jsonGenesisEnemy.eid = index - start_index;
            jsonGenesisEnemy.boss_flg = !allUnit.IsRaidBoss ? 0 : 1;
            jsonGenesisEnemy.hp = (int) allUnit.CurrentStatus.param.hp;
            source.Add(jsonGenesisEnemy);
          }
        }
        requestParam.btlendparam.enemies = source.ToArray<BattleCore.Json_GenesisEnemy>();
      }
      if (used_items != null && used_items.Count != 0)
      {
        requestParam.btlendparam.inputs = new ReqGenesisBossBtlEnd.RequestEndInput[used_items.Count];
        int index = 0;
        foreach (KeyValuePair<OString, OInt> usedItem in used_items)
        {
          requestParam.btlendparam.inputs[index] = new ReqGenesisBossBtlEnd.RequestEndInput();
          requestParam.btlendparam.inputs[index].use = (string) usedItem.Key;
          requestParam.btlendparam.inputs[index].n = (int) usedItem.Value;
          ++index;
        }
      }
      if (missions != null && missions.Length != 0)
      {
        requestParam.btlendparam.missions = new int[missions.Length];
        Array.Copy((Array) missions, (Array) requestParam.btlendparam.missions, missions.Length);
      }
      if (missions_log != null && missions_log.Length != 0)
      {
        requestParam.btlendparam.missions_log = new int[missions_log.Length];
        Array.Copy((Array) missions_log, (Array) requestParam.btlendparam.missions_log, missions_log.Length);
      }
      requestParam.btlendparam.auto_status = record == null || !record.mIsUseAutoPlayMode ? (record == null || record.mIsUseAutoPlayMode ? "none" : "off") : "on";
      this.body = WebAPI.GetRequestString<ReqGenesisBossBtlEnd.RequestParam>(requestParam);
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
      public ReqGenesisBossBtlEnd.RequestEnd btlendparam;
    }

    [Serializable]
    public class RequestEnd
    {
      public BattleCore.Json_GenesisEnemy[] enemies;
      public ReqGenesisBossBtlEnd.RequestEndInput[] inputs;
      public int[] missions;
      public int[] missions_log;
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
      public Json_Gift[] genesis_boss_reward;
    }
  }
}
