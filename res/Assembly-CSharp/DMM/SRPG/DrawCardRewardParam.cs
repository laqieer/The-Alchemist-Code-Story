// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class DrawCardRewardParam
  {
    private string mIname;
    private DrawCardRewardParam.Data[] mRewards;

    public string Iname => this.mIname;

    public List<DrawCardRewardParam.Data> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<DrawCardRewardParam.Data>((IEnumerable<DrawCardRewardParam.Data>) this.mRewards) : new List<DrawCardRewardParam.Data>();
      }
    }

    public void Deserialize(JSON_DrawCardRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (DrawCardRewardParam.Data[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new DrawCardRewardParam.Data[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new DrawCardRewardParam.Data();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref Dictionary<string, DrawCardRewardParam> dict,
      JSON_DrawCardRewardParam[] json)
    {
      if (json == null)
        return;
      if (dict == null)
        dict = new Dictionary<string, DrawCardRewardParam>(json.Length);
      dict.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        DrawCardRewardParam drawCardRewardParam = new DrawCardRewardParam();
        drawCardRewardParam.Deserialize(json[index]);
        if (!dict.ContainsKey(json[index].iname))
          dict.Add(json[index].iname, drawCardRewardParam);
      }
    }

    public static DrawCardRewardParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (DrawCardRewardParam) null;
      Dictionary<string, DrawCardRewardParam> drawCardRewardDict = MonoSingleton<GameManager>.Instance.MasterParam.DrawCardRewardDict;
      if (drawCardRewardDict == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>DrawCardRewardParam/GetParam no data!</color>"));
        return (DrawCardRewardParam) null;
      }
      try
      {
        return drawCardRewardDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<DrawCardRewardParam>(key);
      }
    }

    public class Data
    {
      public int Weight;
      public int ItemType;
      public string ItemIname;
      public int ItemNum;

      public void Deserialize(JSON_DrawCardRewardParam.Data json)
      {
        if (json == null)
          return;
        this.Weight = json.weight;
        this.ItemType = json.item_type;
        this.ItemIname = json.item_iname;
        this.ItemNum = json.item_num;
      }
    }
  }
}
