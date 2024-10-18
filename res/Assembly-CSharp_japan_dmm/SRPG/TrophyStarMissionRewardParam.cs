// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyStarMissionRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TrophyStarMissionRewardParam
  {
    private string mIname;
    private TrophyStarMissionRewardParam.Data[] mRewards;

    public string Iname => this.mIname;

    public List<TrophyStarMissionRewardParam.Data> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<TrophyStarMissionRewardParam.Data>((IEnumerable<TrophyStarMissionRewardParam.Data>) this.mRewards) : new List<TrophyStarMissionRewardParam.Data>();
      }
    }

    public void Deserialize(JSON_TrophyStarMissionRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (TrophyStarMissionRewardParam.Data[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new TrophyStarMissionRewardParam.Data[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new TrophyStarMissionRewardParam.Data();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      JSON_TrophyStarMissionRewardParam[] json,
      ref Dictionary<string, TrophyStarMissionRewardParam> dict)
    {
      if (json == null)
        return;
      if (dict == null)
        dict = new Dictionary<string, TrophyStarMissionRewardParam>(json.Length);
      dict.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        TrophyStarMissionRewardParam missionRewardParam = new TrophyStarMissionRewardParam();
        missionRewardParam.Deserialize(json[index]);
        if (!dict.ContainsKey(json[index].iname))
          dict.Add(json[index].iname, missionRewardParam);
      }
    }

    public static TrophyStarMissionRewardParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (TrophyStarMissionRewardParam) null;
      Dictionary<string, TrophyStarMissionRewardParam> missionRewardDict = MonoSingleton<GameManager>.Instance.MasterParam.TrophyStarMissionRewardDict;
      if (missionRewardDict == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>TrophyStarMissionRewardParam/GetParam no data!</color>"));
        return (TrophyStarMissionRewardParam) null;
      }
      try
      {
        return missionRewardDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<TrophyStarMissionRewardParam>(key);
      }
    }

    public class Data
    {
      public int ItemType;
      public string ItemIname;
      public int ItemNum;

      public void Deserialize(JSON_TrophyStarMissionRewardParam.Data json)
      {
        if (json == null)
          return;
        this.ItemType = json.item_type;
        this.ItemIname = json.item_iname;
        this.ItemNum = json.item_num;
      }
    }
  }
}
