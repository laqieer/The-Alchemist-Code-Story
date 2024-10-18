// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class DrawCardParam
  {
    public string Iname;
    public DateTime BeginAt;
    public DateTime EndAt;
    public DrawCardInfoParam[] mDrawCardInfos;
    public static bool DrawCardEnabled;
    public static int DrawCardStepCount;
    private static DrawCardParam.CardData[] mSelectDrawCards;
    public static int SelectDrawCardIndex = -1;
    private static DrawCardParam.CardData mChoiceDrawCard;
    private static DrawCardParam.CardData[] mRewardDrawCards;

    public List<DrawCardInfoParam> DrawCardList
    {
      get
      {
        return this.mDrawCardInfos != null ? new List<DrawCardInfoParam>((IEnumerable<DrawCardInfoParam>) this.mDrawCardInfos) : new List<DrawCardInfoParam>();
      }
    }

    public void Deserialize(JSON_DrawCardParam json)
    {
      if (json == null)
        return;
      this.Iname = json.iname;
      this.BeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.BeginAt);
      this.EndAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.EndAt);
      this.mDrawCardInfos = (DrawCardInfoParam[]) null;
      if (json.draw_infos == null || json.draw_infos.Length == 0)
        return;
      this.mDrawCardInfos = new DrawCardInfoParam[json.draw_infos.Length];
      for (int index = 0; index < json.draw_infos.Length; ++index)
      {
        this.mDrawCardInfos[index] = new DrawCardInfoParam();
        this.mDrawCardInfos[index].Deserialize(json.draw_infos[index]);
      }
    }

    public bool IsWithinPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.BeginAt <= serverTime && serverTime <= this.EndAt;
    }

    public static void Deserialize(
      ref Dictionary<string, DrawCardParam> dict,
      JSON_DrawCardParam[] json)
    {
      if (json == null)
        return;
      if (dict == null)
        dict = new Dictionary<string, DrawCardParam>(json.Length);
      dict.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        DrawCardParam drawCardParam = new DrawCardParam();
        drawCardParam.Deserialize(json[index]);
        if (!dict.ContainsKey(json[index].iname))
          dict.Add(json[index].iname, drawCardParam);
      }
    }

    public static DrawCardParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (DrawCardParam) null;
      Dictionary<string, DrawCardParam> drawCardDict = MonoSingleton<GameManager>.Instance.MasterParam.DrawCardDict;
      if (drawCardDict == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>DrawCardParam/GetParam no data!</color>"));
        return (DrawCardParam) null;
      }
      try
      {
        return drawCardDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<DrawCardParam>(key);
      }
    }

    public static DrawCardParam GetActiveDrawCardParam()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (DrawCardParam) null;
      Dictionary<string, DrawCardParam> drawCardDict = MonoSingleton<GameManager>.Instance.MasterParam.DrawCardDict;
      if (drawCardDict == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>DrawCardParam/GetActiveDrawCardParam no data!</color>"));
        return (DrawCardParam) null;
      }
      foreach (DrawCardParam activeDrawCardParam in drawCardDict.Values)
      {
        if (activeDrawCardParam.IsWithinPeriod())
          return activeDrawCardParam;
      }
      return (DrawCardParam) null;
    }

    public static List<DrawCardRewardParam.Data> GetCurrentDrawCardRewardList()
    {
      DrawCardParam activeDrawCardParam = DrawCardParam.GetActiveDrawCardParam();
      if (activeDrawCardParam == null)
        return (List<DrawCardRewardParam.Data>) null;
      if (DrawCardParam.DrawCardStepCount <= 0 || DrawCardParam.DrawCardStepCount > activeDrawCardParam.DrawCardList.Count)
        return (List<DrawCardRewardParam.Data>) null;
      int index = DrawCardParam.DrawCardStepCount - 1;
      DrawCardInfoParam drawCard = activeDrawCardParam.DrawCardList[index];
      return drawCard.DcReward == null ? (List<DrawCardRewardParam.Data>) null : drawCard.DcReward.RewardList;
    }

    public static List<DrawCardParam.CardData> SelectDrawCardList
    {
      get
      {
        return DrawCardParam.mSelectDrawCards != null ? new List<DrawCardParam.CardData>((IEnumerable<DrawCardParam.CardData>) DrawCardParam.mSelectDrawCards) : new List<DrawCardParam.CardData>();
      }
    }

    public static bool EntrySelectDrawCards(ReqDrawCard.CardInfo[] select_cards)
    {
      DrawCardParam.ClearSelectDrawCard();
      if (select_cards == null || select_cards.Length == 0)
        return false;
      DrawCardParam.mSelectDrawCards = new DrawCardParam.CardData[select_cards.Length];
      for (int index = 0; index < select_cards.Length; ++index)
      {
        ReqDrawCard.CardInfo selectCard = select_cards[index];
        if (selectCard == null || selectCard.draw_card_reward == null)
        {
          DrawCardParam.mSelectDrawCards = (DrawCardParam.CardData[]) null;
          return false;
        }
        ReqDrawCard.CardInfo.Card drawCardReward = selectCard.draw_card_reward;
        DrawCardParam.mSelectDrawCards[index] = new DrawCardParam.CardData(drawCardReward.item_type, drawCardReward.item_iname, drawCardReward.item_num, selectCard.is_miss);
      }
      return true;
    }

    public static void ClearSelectDrawCard()
    {
      DrawCardParam.mSelectDrawCards = (DrawCardParam.CardData[]) null;
    }

    public static DrawCardParam.CardData ChoiceDrawCard => DrawCardParam.mChoiceDrawCard;

    public static bool EntryChoiceDrawCard(ReqDrawCard.CardInfo draw_card_info)
    {
      DrawCardParam.ClearChoiceDrawCard();
      if (draw_card_info == null || draw_card_info.draw_card_reward == null)
        return false;
      ReqDrawCard.CardInfo.Card drawCardReward = draw_card_info.draw_card_reward;
      DrawCardParam.mChoiceDrawCard = new DrawCardParam.CardData(drawCardReward.item_type, drawCardReward.item_iname, drawCardReward.item_num, draw_card_info.is_miss);
      return true;
    }

    public static void ClearChoiceDrawCard()
    {
      DrawCardParam.mChoiceDrawCard = (DrawCardParam.CardData) null;
    }

    public static List<DrawCardParam.CardData> RewardDrawCardList
    {
      get
      {
        return DrawCardParam.mRewardDrawCards != null ? new List<DrawCardParam.CardData>((IEnumerable<DrawCardParam.CardData>) DrawCardParam.mRewardDrawCards) : new List<DrawCardParam.CardData>();
      }
    }

    public static bool EntryRewardDrawCards(ReqDrawCard.CardInfo.Card[] reward_cards)
    {
      DrawCardParam.ClearRewardDrawCard();
      if (reward_cards == null)
        return false;
      DrawCardParam.mRewardDrawCards = new DrawCardParam.CardData[reward_cards.Length];
      for (int index = 0; index < reward_cards.Length; ++index)
      {
        ReqDrawCard.CardInfo.Card rewardCard = reward_cards[index];
        if (rewardCard == null)
        {
          DrawCardParam.mRewardDrawCards = (DrawCardParam.CardData[]) null;
          return false;
        }
        DrawCardParam.mRewardDrawCards[index] = new DrawCardParam.CardData(rewardCard.item_type, rewardCard.item_iname, rewardCard.item_num);
      }
      return true;
    }

    public static void ClearRewardDrawCard()
    {
      DrawCardParam.mRewardDrawCards = (DrawCardParam.CardData[]) null;
    }

    public class CardData
    {
      public int ItemType;
      public string ItemIname;
      public int ItemNum;
      public bool IsMiss;

      public CardData()
      {
      }

      public CardData(int type, string iname, int num, int is_miss = 0)
      {
        this.ItemType = type;
        this.ItemIname = iname;
        this.ItemNum = num;
        this.IsMiss = is_miss != 0;
      }
    }
  }
}
