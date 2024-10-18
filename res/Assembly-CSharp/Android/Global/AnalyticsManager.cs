// Decompiled with JetBrains decompiler
// Type: AnalyticsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsManager
{
  private static readonly List<AnalyticsManager.TrackingTriggerEventData> TutorialEventTriggers = new List<AnalyticsManager.TrackingTriggerEventData>()
  {
    new AnalyticsManager.TrackingTriggerEventData("splash", "splash", "1"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download", "download_main", "2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download_Dialog", "download_bg", "3"),
    new AnalyticsManager.TrackingTriggerEventData("BackgroundDownloaderEnabled", "download_bg_yes", "3.1"),
    new AnalyticsManager.TrackingTriggerEventData("BackgroundDownloaderDisabled", "download_bg_no", "3.2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download_Start", "download_bg_start", "4"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_Intro", "movie_intro", "5"),
    new AnalyticsManager.TrackingTriggerEventData("tut001a.001", "scene_thecode", "6"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_BattleGrid_Show.ToString(), "guide_battle_move", "7"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.003", "guide_battle_movebehind", "7.1"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.004", "guide_battle_basicshield", "7.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.005", "guide_battle_swordcrush", "7.3"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.006", "guide_battle_activateskill", "7.4"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.007", "guide_battle_endmove", "7.5"),
    new AnalyticsManager.TrackingTriggerEventData("guide_battle_freeplay", "guide_battle_freeplay", "7.6"),
    new AnalyticsManager.TrackingTriggerEventData("tut001b.001", "dialogue_start", "7.7"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiVSDias.ToString(), "skipdialog_after_logi_vs_dias", "7.7.1"),
    new AnalyticsManager.TrackingTriggerEventData("tut001b.007a", "dialogue_end", "7.8"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_AnimeIntro", "movie_anime", "8"),
    new AnalyticsManager.TrackingTriggerEventData("tut002a.001", "scene_resistance", "9"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSVlad.ToString(), "skipdialog_before_logidias_vs_vlad", "9.1"),
    new AnalyticsManager.TrackingTriggerEventData("tut003a.001", "scene_trepidation", "10"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSVlad.ToString(), "skipdialog_after_logidias_vs_vlad", "10.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1.ToString(), "skipdialog_after_logidias_vs_neville_pt1", "10.2"),
    new AnalyticsManager.TrackingTriggerEventData("0_6b_2d.001", "scene_ouroboros", "11"),
    new AnalyticsManager.TrackingTriggerEventData("0_6b_2d.017", "guide_summon1", "12"),
    new AnalyticsManager.TrackingTriggerEventData("tut004a.001", "scene_started", "13"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2.ToString(), "skipdialog_before_logidias_vs_neville_pt2", "13.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2.ToString(), "skipdialog_after_logidias_vs_neville_pt2", "13.2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_World", "movie_world", "14"),
    new AnalyticsManager.TrackingTriggerEventData("0_8_2d.001", "scene_throne", "15"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Show.ToString(), "confirm_skip1", "16"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Continue.ToString(), "confirm_skip1_yes", "16.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Cancel.ToString(), "confirm_skip1_no", "16.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.001", "guide_missions", "17"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.007", "guide_summon2", "18"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.008", "guide_summon2_use", "19"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.009", "guide_summon2_get", "20"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.011", "guide_challenges", "21"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Show.ToString(), "confirm_skip2", "22"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Continue.ToString(), "confirm_skip2_yes", "22.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Cancel.ToString(), "confirm_skip2_no", "22.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.015", "guide_units", "23"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.032", "guide_quests", "24"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.035", "guide_party", "25"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.038", "end", "26")
  };
  private static readonly List<AnalyticsManager.TrackingTriggerEventData> MissionEventTriggers = new List<AnalyticsManager.TrackingTriggerEventData>()
  {
    new AnalyticsManager.TrackingTriggerEventData("QE_ST_NO_010001", "first_quest_clear", "1")
  };
  private static readonly string[] GachaCostType = new string[8]
  {
    "None",
    "Gems",
    "Paid Gems",
    "Zeni",
    "Ticket",
    "Free Gems",
    "Zeni",
    "All"
  };
  private static readonly string[] GachaSummonType = new string[6]
  {
    "None",
    "Rare",
    "Equipment",
    "Ticket",
    "Normal",
    "Special"
  };
  private static readonly string[] NonPremiumCurrencyString = new string[4]
  {
    "zeni",
    "ticket",
    "ap",
    "item"
  };
  private static readonly List<int> LevelUpThresholdReportingTriggers = new List<int>()
  {
    15,
    40,
    60
  };
  private const bool IS_ANALYTICS_REPORT_TO_DEV_OR_SHUTOFF = false;
  private const string GCM_SENDER_ID = "813126952066";
  private const bool IS_ANALYTICS_DEBUG_MODE = false;
  private static string _lastKnownUserID;
  private static List<AnalyticsReporterBase> _analyticsReporters;
  private static AnalyticsManager.SummonData lastKnownSummonData;

  private static bool HasPlayerCompletedGameplayPortionOfTutorial
  {
    get
    {
      return !MonoSingleton<GameManager>.Instance.IsTutorial();
    }
  }

  public static void TrackTutorialEventGeneric(AnalyticsManager.TrackingType inTrackingType)
  {
    switch (inTrackingType)
    {
      case AnalyticsManager.TrackingType.StaminaReward_Video:
        AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.AP, (long) GlobalVars.LastReward.Get().Stamina, "Video Ads", (string) null);
        break;
      case AnalyticsManager.TrackingType.StaminaReward_Milestone:
        AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.AP, (long) GlobalVars.LastReward.Get().Stamina, "Milestone Rewards", (string) null);
        break;
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Continue:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Cancel:
      case AnalyticsManager.TrackingType.Tutorial_Download:
      case AnalyticsManager.TrackingType.Tutorial_Download_Dialog:
      case AnalyticsManager.TrackingType.Tutorial_Download_Start:
      case AnalyticsManager.TrackingType.Tutorial_Movie_Intro:
      case AnalyticsManager.TrackingType.Tutorial_Movie_AnimeIntro:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Continue:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Cancel:
      case AnalyticsManager.TrackingType.Tutorial_Movie_World:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiVSDias:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSVlad:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSVlad:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Show:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Show:
      case AnalyticsManager.TrackingType.Tutorial_BattleGrid_Show:
        AnalyticsManager.TrackTutorialAnalyticsEvent(inTrackingType.ToString());
        break;
      case AnalyticsManager.TrackingType.Player_New:
        AnalyticsManager.RecordNewPlayerLogin(AnalyticsManager._lastKnownUserID);
        break;
      case AnalyticsManager.TrackingType.Player_Guest:
        AnalyticsManager.RecordGuestLogin(AnalyticsManager._lastKnownUserID);
        break;
      case AnalyticsManager.TrackingType.Player_FB:
        AnalyticsManager.RecordFacebookLogin(AnalyticsManager._lastKnownUserID);
        break;
      case AnalyticsManager.TrackingType.Tutorial_BGDLC:
        if (BackgroundDownloader.Instance.IsEnabled)
        {
          AnalyticsManager.TrackTutorialAnalyticsEvent("BackgroundDownloaderEnabled");
          break;
        }
        AnalyticsManager.TrackTutorialAnalyticsEvent("BackgroundDownloaderDisabled");
        break;
      default:
        throw new ArgumentOutOfRangeException("Unknown Tutorial Analytics Event");
    }
  }

  private static string GetGachaSummonCostTypeString(GachaButton.GachaCostType inGachaCostType)
  {
    int num = (int) inGachaCostType;
    if (num > -1 && num < AnalyticsManager.GachaCostType.Length)
      return AnalyticsManager.GachaCostType[(int) inGachaCostType];
    return (string) null;
  }

  private static string GetGachaSummonTypeString(GachaWindow.GachaTabCategory inGachaType)
  {
    int num = (int) inGachaType;
    if (num > -1 && num < AnalyticsManager.GachaSummonType.Length)
      return AnalyticsManager.GachaSummonType[(int) inGachaType];
    return (string) null;
  }

  private static string GetNonPremiumCurrencyStringType(AnalyticsManager.NonPremiumCurrencyType inCurrencyType)
  {
    int num = (int) inCurrencyType;
    if (num > -1 && num < AnalyticsManager.NonPremiumCurrencyString.Length)
      return AnalyticsManager.NonPremiumCurrencyString[(int) inCurrencyType];
    return (string) null;
  }

  private static List<AnalyticsReporterBase> analyticsReporters
  {
    get
    {
      if (AnalyticsManager._analyticsReporters == null)
      {
        AnalyticsManager._analyticsReporters = new List<AnalyticsReporterBase>();
        AnalyticsManager._analyticsReporters.Add((AnalyticsReporterBase) new AnalyticsReporterTapjoy());
        AnalyticsManager._analyticsReporters.Add((AnalyticsReporterBase) new AnalyticsReporterAppsFlyer());
        Debug.LogWarning((object) "[Analytics] Lazy intialization");
      }
      return AnalyticsManager._analyticsReporters;
    }
  }

  public static void Setup(string inUserID, bool inIsPayingPlayer)
  {
    AnalyticsManager._lastKnownUserID = inUserID;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.Setup(inUserID, inIsPayingPlayer, "813126952066", false, false);
    }
  }

  public static void AddPlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.AddPlacementFlow(inPlacementWantedFlowChangeHandler);
    }
  }

  public static void RemovePlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.RemovePlacementFlow(inPlacementWantedFlowChangeHandler);
    }
  }

  public static void SetSummonTracking(GachaButton.GachaCostType inCostType, GachaWindow.GachaTabCategory inTabCategory, string inGateID, bool inIsFree, int inSummonCost, int inNumberOfThingsSummoned, int inCurrentSummonStepIndex)
  {
    string str;
    if (inIsFree)
    {
      switch (inCostType)
      {
        case GachaButton.GachaCostType.COIN:
          str = "Rare (Free)";
          break;
        case GachaButton.GachaCostType.GOLD:
          str = "Normal (Free)";
          break;
        default:
          str = string.Empty;
          break;
      }
    }
    else
    {
      switch (inCostType)
      {
        case GachaButton.GachaCostType.COIN:
          str = "Rare (Paid)";
          break;
        case GachaButton.GachaCostType.COIN_P:
          str = "Rare (Discount)";
          break;
        case GachaButton.GachaCostType.GOLD:
          str = "Normal (Zeni)";
          break;
        default:
          str = string.Empty;
          break;
      }
    }
    AnalyticsManager.lastKnownSummonData = new AnalyticsManager.SummonData();
    AnalyticsManager.lastKnownSummonData.main_type = AnalyticsManager.GetGachaSummonTypeString(inTabCategory);
    AnalyticsManager.lastKnownSummonData.sub_type = str;
    AnalyticsManager.lastKnownSummonData.currency_used = AnalyticsManager.GetGachaSummonCostTypeString(inCostType);
    AnalyticsManager.lastKnownSummonData.currency_amount = inSummonCost;
    AnalyticsManager.lastKnownSummonData.gate_id = inGateID;
    AnalyticsManager.lastKnownSummonData.step_id = inCurrentSummonStepIndex;
    AnalyticsManager.lastKnownSummonData.step_reward_id = string.Empty;
    AnalyticsManager.lastKnownSummonData.step_reward_amount = inNumberOfThingsSummoned;
  }

  public static void TrackSummonComplete()
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackSummonData(AnalyticsManager.lastKnownSummonData.main_type, AnalyticsManager.lastKnownSummonData.sub_type, AnalyticsManager.lastKnownSummonData.currency_used, AnalyticsManager.lastKnownSummonData.currency_amount, AnalyticsManager.lastKnownSummonData.gate_id, AnalyticsManager.lastKnownSummonData.step_id, AnalyticsManager.lastKnownSummonData.step_reward_id, AnalyticsManager.lastKnownSummonData.step_reward_amount);
    }
  }

  public static void TrackOriginalCurrencyUse(ESaleType inSaleType, int inAmount, string inSinkSource)
  {
    switch (inSaleType)
    {
      case ESaleType.Gold:
        AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) inAmount, inSinkSource, (string) null);
        break;
      case ESaleType.Coin:
        AnalyticsManager.TrackFreePremiumCurrencyUse((long) inAmount, inSinkSource);
        break;
      case ESaleType.TourCoin:
      case ESaleType.ArenaCoin:
      case ESaleType.PiecePoint:
      case ESaleType.MultiCoin:
      case ESaleType.EventCoin:
        AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Item, (long) inAmount, inSinkSource, inSaleType.ToString());
        break;
      case ESaleType.Coin_P:
        AnalyticsManager.TrackPaidPremiumCurrencyUse((long) inAmount, inSinkSource);
        break;
      default:
        Debug.LogWarning((object) string.Format("[Analytics]Currency not handled\n SaleType: {0}\ninAmount: {1}\ninSinkSource: {2}", (object) inSaleType, (object) inAmount, (object) inSinkSource));
        break;
    }
  }

  public static void TrackPaidPremiumCurrencyUse(long inAmount, string inSinkSource)
  {
    if (inAmount <= 0L)
      return;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackPaidPremiumCurrencyTransaction("currency.use.gem", inSinkSource, inAmount);
    }
  }

  public static void TrackFreePremiumCurrencyUse(long inAmount, string inSinkSource)
  {
    if (inAmount <= 0L)
      return;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackFreePremiumCurrencyTransaction("currency.use.gem", inSinkSource, inAmount);
    }
  }

  public static void TrackPaidPremiumCurrencyObtain(long inAmount, string inObtainSource)
  {
    if (inAmount <= 0L)
      return;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackPaidPremiumCurrencyTransaction("currency.obtain.gem", inObtainSource, inAmount);
    }
  }

  public static void TrackFreePremiumCurrencyObtain(long inAmount, string inObtainSource)
  {
    if (inAmount <= 0L)
      return;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackFreePremiumCurrencyTransaction("currency.obtain.gem", inObtainSource, inAmount);
    }
  }

  public static void TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inObtainSource, string inUniqueCurrencyID = null)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackNonPremiumCurrencyTransaction("currency.obtain.", inCurrencyType, inAmount, inObtainSource, inUniqueCurrencyID);
  }

  public static void TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inSinkSource, string inUniqueCurrencyID = null)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackNonPremiumCurrencyTransaction("currency.use.", inCurrencyType, inAmount, inSinkSource, inUniqueCurrencyID);
  }

  private static void TrackNonPremiumCurrencyTransaction(string transactionName, AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inObtainSource, string inUniqueCurrencyID)
  {
    transactionName += AnalyticsManager.GetNonPremiumCurrencyStringType(inCurrencyType);
    string inUniqueCurrencyKey = string.Empty;
    if (!string.IsNullOrEmpty(inUniqueCurrencyID) && (inCurrencyType == AnalyticsManager.NonPremiumCurrencyType.Item || inCurrencyType == AnalyticsManager.NonPremiumCurrencyType.SummonTicket))
      inUniqueCurrencyKey = inCurrencyType != AnalyticsManager.NonPremiumCurrencyType.Item ? "ticket_id" : "item_id";
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackNonPremiumCurrencyTransaction(transactionName, inObtainSource, inUniqueCurrencyKey, inUniqueCurrencyID, inAmount);
    }
  }

  public static void TrackPurchase(string inProductID, string inCurrencyCode, double inPrice)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackPurchaseEvent(inProductID, inCurrencyCode, inPrice);
    }
  }

  public static void TrackPlayerLevelUp(int inPreviousLevel, int inCurrentLevel)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackLevelUpEvent(inCurrentLevel);
    }
    using (List<int>.Enumerator enumerator = AnalyticsManager.LevelUpThresholdReportingTriggers.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        if (inPreviousLevel < current && current <= inCurrentLevel)
        {
          string inReportingName = "player_level_" + (object) current;
          current.ToString();
          AnalyticsManager.CallTrackEventOnReporters(AnalyticsManager.analyticsReporters, inReportingName, new Dictionary<string, object>()
          {
            {
              "adcampaign_level_achieved",
              (object) current
            }
          });
        }
      }
    }
  }

  public static void TrackTutorialAnalyticsEvent(string inTag)
  {
    AnalyticsManager.TrackingTriggerEventData triggerEventData1 = AnalyticsManager.TutorialEventTriggers.Find((Predicate<AnalyticsManager.TrackingTriggerEventData>) (triggerEventData => triggerEventData.ID.Equals(inTag)));
    if (triggerEventData1.Equals((object) new AnalyticsManager.TrackingTriggerEventData()))
      return;
    string str = "funnel.tutorial." + triggerEventData1.ReportingName;
    if (AnalyticsManager.HasPlayerCompletedGameplayPortionOfTutorial || PlayerPrefs.HasKey(str))
      return;
    PlayerPrefs.SetString(str, string.Empty);
    AnalyticsManager.CallTrackEventOnReporters(AnalyticsManager.analyticsReporters, str, new Dictionary<string, object>()
    {
      {
        "step_number",
        (object) triggerEventData1.StepNumber
      }
    });
  }

  public static void TrackMissionAnalyticsEvent(string inMissionID)
  {
    AnalyticsManager.TrackingTriggerEventData triggerEventData1 = AnalyticsManager.MissionEventTriggers.Find((Predicate<AnalyticsManager.TrackingTriggerEventData>) (triggerEventData => triggerEventData.ID.Equals(inMissionID)));
    if (triggerEventData1.Equals((object) new AnalyticsManager.TrackingTriggerEventData()))
      return;
    string reportingName = triggerEventData1.ReportingName;
    if (PlayerPrefs.HasKey(reportingName))
      return;
    PlayerPrefs.SetString(reportingName, string.Empty);
    AnalyticsManager.CallTrackEventOnReporters(AnalyticsManager.analyticsReporters, reportingName, new Dictionary<string, object>()
    {
      {
        "mission",
        (object) inMissionID
      }
    });
  }

  private static void RecordNewPlayerLogin(string inUserID)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.RecordNewPlayerLogin(inUserID);
    }
  }

  private static void RecordGuestLogin(string inUserID)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.RecordGuestLogin(inUserID);
    }
  }

  private static void RecordFacebookLogin(string inUserID)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.RecordFacebookLogin(inUserID);
    }
  }

  public static string LastKnownSessionIDOfShownOfferwall { get; private set; }

  public static void AttemptToShowPlacement(string inMileStoneName, Action inCallbackForPlacementBeingShown, string inNetworkSessionID)
  {
    DebugUtility.LogWarning("<color=red>Milestone:" + inMileStoneName);
    AnalyticsManager.LastKnownSessionIDOfShownOfferwall = inNetworkSessionID;
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.AttemptToShowPlacement(inMileStoneName, inCallbackForPlacementBeingShown);
    }
  }

  public static void OnApplicationFocus(bool inIsFocus)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.OnApplicationFocus(inIsFocus);
    }
  }

  private static void CallTrackEventOnReporters(List<AnalyticsReporterBase> inReporterList, string inReportingName, Dictionary<string, object> inTrackInfo)
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = inReporterList.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.TrackEvent(inReportingName, inTrackInfo);
    }
  }

  public static bool HasOfferwallToShow()
  {
    using (List<AnalyticsReporterBase>.Enumerator enumerator = AnalyticsManager.analyticsReporters.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.HasOfferwallToShow())
          return true;
      }
    }
    return false;
  }

  private struct TrackingTriggerEventData
  {
    public readonly string ID;
    public readonly string ReportingName;
    public readonly string StepNumber;

    public TrackingTriggerEventData(string inID, string inReportingName, string inStepNumber)
    {
      this.ID = inID;
      this.ReportingName = inReportingName;
      this.StepNumber = inStepNumber;
    }
  }

  public enum TrackingType
  {
    StaminaReward_Video,
    StaminaReward_Milestone,
    Tutorial_HomeScreen_BasicGuideDialog_Continue,
    Tutorial_HomeScreen_BasicGuideDialog_Cancel,
    Tutorial_Download,
    Tutorial_Download_Dialog,
    Tutorial_Download_Start,
    Tutorial_Movie_Intro,
    Tutorial_Movie_AnimeIntro,
    Tutorial_HomeScreen_UnitsGuideDialog_Continue,
    Tutorial_HomeScreen_UnitsGuideDialog_Cancel,
    Player_New,
    Player_Guest,
    Player_FB,
    Tutorial_Movie_World,
    Tutorial_BGDLC,
    Tutorial_SkipDialog_AfterLogiVSDias,
    Tutorial_SkipDialog_BeforeLogiDiasVSVlad,
    Tutorial_SkipDialog_AfterLogiDiasVSVlad,
    Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1,
    Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2,
    Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2,
    Tutorial_HomeScreen_BasicGuideDialog_Show,
    Tutorial_HomeScreen_UnitsGuideDialog_Show,
    Tutorial_BattleGrid_Show,
  }

  public enum NonPremiumCurrencyType
  {
    Zeni,
    SummonTicket,
    AP,
    Item,
  }

  private struct SummonData
  {
    public string main_type;
    public string sub_type;
    public string currency_used;
    public string step_reward_id;
    public string gate_id;
    public int step_id;
    public int currency_amount;
    public int step_reward_amount;
  }
}
