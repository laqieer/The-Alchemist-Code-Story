// Decompiled with JetBrains decompiler
// Type: SRPG.RuneMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneMaterial
  {
    public short rarity;
    public short[] enhance_probability;
    public RuneCost[] enh_cost;
    public RuneCost[] evo_cost;
    public int disassembly_zeny;
    public RuneDisassembly disassembly = new RuneDisassembly();
    public int[] magnification;
    public RuneCost[] reset_base_param_costs;
    public RuneCost[] reset_evo_status_costs;
    public RuneCost[] param_enh_evo_costs;

    public bool Deserialize(JSON_RuneMaterial json, MasterParam master = null)
    {
      if (master == null)
        master = MonoSingleton<GameManager>.Instance.MasterParam;
      if (master == null)
      {
        DebugUtility.LogError("GameManager.Instance.MasterParam を読み込んだ後に RuneMaterial をデシリアライズしてください");
        return false;
      }
      this.rarity = (short) json.rarity;
      if (json.enhance_probability != null)
      {
        this.enhance_probability = new short[json.enhance_probability.Length];
        for (int index = 0; index < json.enhance_probability.Length; ++index)
          this.enhance_probability[index] = (short) json.enhance_probability[index];
      }
      else
        DebugUtility.LogError("Failed read json.enhance_probability.");
      if (json.enh_cost != null)
      {
        this.enh_cost = new RuneCost[json.enh_cost.Length];
        for (int index = 0; index < json.enh_cost.Length; ++index)
        {
          RuneCost runeCost = master.GetRuneCost(json.enh_cost[index]);
          if (runeCost == null)
            DebugUtility.LogError(json.enh_cost[index] + "がRuneCostシートに存在しません");
          this.enh_cost[index] = runeCost;
        }
      }
      else
        DebugUtility.LogError("Failed read json.enh_cost.");
      if (json.evo_cost != null)
      {
        this.evo_cost = new RuneCost[json.evo_cost.Length];
        for (int index = 0; index < json.evo_cost.Length; ++index)
        {
          RuneCost runeCost = master.GetRuneCost(json.evo_cost[index]);
          if (runeCost == null)
            DebugUtility.LogError(json.evo_cost[index] + "がRuneCostシートに存在しません");
          this.evo_cost[index] = runeCost;
        }
      }
      else
        DebugUtility.LogError("Failed read json.evo_cost.");
      this.disassembly_zeny = json.disassembly_zeny;
      this.disassembly.Deserialize(json, master);
      this.magnification = json.magnification;
      if (json.reset_base_param_costs != null)
      {
        this.reset_base_param_costs = new RuneCost[json.reset_base_param_costs.Length];
        for (int index = 0; index < json.reset_base_param_costs.Length; ++index)
        {
          RuneCost runeCost = master.GetRuneCost(json.reset_base_param_costs[index]);
          if (runeCost == null)
            DebugUtility.LogError(json.reset_base_param_costs[index] + "がRuneCostシートに存在しません");
          this.reset_base_param_costs[index] = runeCost;
        }
      }
      else
        DebugUtility.LogError("Failed read json.reset_base_param_costs.");
      if (json.reset_evo_status_costs != null)
      {
        this.reset_evo_status_costs = new RuneCost[json.reset_evo_status_costs.Length];
        for (int index = 0; index < json.reset_evo_status_costs.Length; ++index)
        {
          RuneCost runeCost = master.GetRuneCost(json.reset_evo_status_costs[index]);
          if (runeCost == null)
            DebugUtility.LogError(json.reset_evo_status_costs[index] + "がRuneCostシートに存在しません");
          this.reset_evo_status_costs[index] = runeCost;
        }
      }
      else
        DebugUtility.LogError("Failed read json.reset_evo_status_costs.");
      if (json.param_enh_evo_costs != null)
      {
        this.param_enh_evo_costs = new RuneCost[json.param_enh_evo_costs.Length];
        for (int index = 0; index < json.param_enh_evo_costs.Length; ++index)
        {
          RuneCost runeCost = master.GetRuneCost(json.param_enh_evo_costs[index]);
          if (runeCost == null)
            DebugUtility.LogError(json.param_enh_evo_costs[index] + "がRuneCostシートに存在しません");
          this.param_enh_evo_costs[index] = runeCost;
        }
      }
      return true;
    }
  }
}
