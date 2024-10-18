// Decompiled with JetBrains decompiler
// Type: SRPG.RuneUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class RuneUtility
  {
    public static readonly int CAN_USE_GAUGE_PERCENTAGE = 100;

    public static List<RuneSetEff> GetEnableRuneSetEffects(RuneData[] runes)
    {
      return RuneUtility.GetEnableRuneSetEffects(((IEnumerable<RuneData>) runes).Select<RuneData, RuneParam>((Func<RuneData, RuneParam>) (rune => rune?.RuneParam)).ToArray<RuneParam>());
    }

    public static List<RuneSetEff> GetEnableRuneSetEffects(RuneParam[] runeParams)
    {
      if (runeParams == null)
        return (List<RuneSetEff>) null;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (List<RuneSetEff>) null;
      List<RuneSetEff> enableRuneSetEffects = new List<RuneSetEff>();
      Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
      for (int index = 0; index < runeParams.Length; ++index)
      {
        if (runeParams[index] != null)
        {
          if (dictionary1.ContainsKey(runeParams[index].seteff_type))
          {
            Dictionary<int, int> dictionary2;
            int seteffType;
            (dictionary2 = dictionary1)[seteffType = runeParams[index].seteff_type] = dictionary2[seteffType] + 1;
          }
          else
            dictionary1[runeParams[index].seteff_type] = 1;
        }
      }
      foreach (KeyValuePair<int, int> keyValuePair in dictionary1)
      {
        RuneSetEff runeSetEff = instanceDirect.MasterParam.GetRuneSetEff(keyValuePair.Key);
        if (runeSetEff != null)
        {
          int num = keyValuePair.Value / (int) runeSetEff.cost;
          for (int index = 0; index < num; ++index)
            enableRuneSetEffects.Add(runeSetEff);
        }
      }
      return enableRuneSetEffects;
    }

    public static int CountRuneNum(Json_Item[] items)
    {
      int num = 0;
      if (items == null)
        return num;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index = 0; index < items.Length; ++index)
      {
        if (string.IsNullOrEmpty(items[index].iname))
        {
          DebugUtility.LogError(string.Format("Json_Item[{0}] iname が null です。", (object) index));
        }
        else
        {
          ItemParam itemParam;
          if ((itemParam = masterParam.GetItemParam(items[index].iname)) != null && itemParam.type == EItemType.Rune)
            ++num;
        }
      }
      return num;
    }

    public static int CountRuneNum(RewardData reward)
    {
      int num = 0;
      if (reward == null)
        return num;
      foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in reward.GiftRecieveItemDataDic)
      {
        ItemParam itemParam;
        if (keyValuePair.Value.type == GiftTypes.Item && (itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(keyValuePair.Key)) != null && itemParam.type == EItemType.Rune)
          num += keyValuePair.Value.num;
      }
      return num;
    }

    public static int CountRuneNum(BattleCore.Json_BtlInfo[] battleInfo)
    {
      int num = 0;
      if (battleInfo == null)
        return num;
      for (int index1 = 0; index1 < battleInfo.Length; ++index1)
      {
        BattleCore.Json_BtlInfo jsonBtlInfo = battleInfo[index1];
        if (jsonBtlInfo.drops != null)
        {
          for (int index2 = 0; index2 < jsonBtlInfo.drops.Length; ++index2)
          {
            ItemParam itemParam;
            if (jsonBtlInfo.drops[index2].dropItemType == EBattleRewardType.Item && (itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(jsonBtlInfo.drops[index2].iname)) != null && itemParam.type == EItemType.Rune)
              num += jsonBtlInfo.drops[index2].num;
          }
        }
      }
      return num;
    }

    public static int CountRuneNum(List<BattleCore.DropItemParam> items)
    {
      int num = 0;
      if (items == null)
        return num;
      for (int index = 0; index < items.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = items[index];
        if (dropItemParam.IsItem && dropItemParam.itemParam.type == EItemType.Rune)
          ++num;
      }
      return num;
    }

    public static int CountRuneNum(BattleCore.Json_BtlDrop[][] drops)
    {
      int num = 0;
      if (drops == null)
        return num;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index1 = 0; index1 < drops.Length; ++index1)
      {
        if (drops[index1] != null)
        {
          for (int index2 = 0; index2 < drops[index1].Length; ++index2)
          {
            BattleCore.Json_BtlDrop jsonBtlDrop;
            ItemParam itemParam;
            if ((jsonBtlDrop = drops[index1][index2]) != null && jsonBtlDrop.dropItemType == EBattleRewardType.Item && (itemParam = masterParam.GetItemParam(jsonBtlDrop.iname)) != null && itemParam.type == EItemType.Rune)
              ++num;
          }
        }
      }
      return num;
    }

    public static bool IsCanUseGauge(BindRuneData rune)
    {
      int num1 = RuneUtility.CalcSuccessPercentage(rune);
      if (RuneUtility.CAN_USE_GAUGE_PERCENTAGE <= num1)
        return false;
      int num2 = RuneUtility.CalcPlayerGauge(rune);
      return RuneUtility.CAN_USE_GAUGE_PERCENTAGE <= num2 + num1;
    }

    public static int CalcPlayerGauge(BindRuneData rune)
    {
      int rarity = rune.Rarity;
      List<RuneEnforceGaugeData> runeEnforceGauge = MonoSingleton<GameManager>.Instance.Player.GetRuneEnforceGauge();
      for (int index = 0; index < runeEnforceGauge.Count; ++index)
      {
        if ((int) runeEnforceGauge[index].rare == rarity)
          return (int) runeEnforceGauge[index].val;
      }
      return 0;
    }

    public static int CalcSuccessPercentage(BindRuneData bind_rune)
    {
      if (bind_rune == null)
        return 0;
      RuneData rune = bind_rune.Rune;
      if (rune == null)
        return 0;
      RuneMaterial runeMaterial = rune.RuneMaterial;
      if (runeMaterial == null)
        return 0;
      short[] enhanceProbability = runeMaterial.enhance_probability;
      int index = (int) rune.enforce;
      if (0 >= enhanceProbability.Length)
        return 0;
      if (index >= enhanceProbability.Length)
        index = enhanceProbability.Length - 1;
      return (int) enhanceProbability[index];
    }
  }
}
