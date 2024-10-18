// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisBossBtlEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPG
{
  public class ReqGenesisBossBtlEnd : WebAPI
  {
    public ReqGenesisBossBtlEnd(long btlid, BtlResultTypes result, List<Unit> all_units, int start_index, Dictionary<OString, OInt> used_items, Network.ResponseCallback response)
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
            source.Add(new BattleCore.Json_GenesisEnemy()
            {
              eid = index - start_index,
              boss_flg = !allUnit.IsRaidBoss ? 0 : 1,
              hp = (int) allUnit.CurrentStatus.param.hp
            });
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
      this.body = WebAPI.GetRequestString<ReqGenesisBossBtlEnd.RequestParam>(requestParam);
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
