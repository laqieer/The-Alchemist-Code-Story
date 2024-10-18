// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1000, "デイリー", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "レコード", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(2, "初期化(ポート)", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "更新(ポート)", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1002, "ポートデイリー", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "報酬を一括受け取り", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "報酬を一括受け取り(ポート)", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(2000, "報酬を受け取り", FlowNode.PinTypes.Output, 2000)]
  [FlowNode.Pin(2001, "ガチャへ移動", FlowNode.PinTypes.Output, 2001)]
  [FlowNode.Pin(2002, "クエスト選択へ移動", FlowNode.PinTypes.Output, 2002)]
  [FlowNode.Pin(2003, "ゴールド購入画面へ移動", FlowNode.PinTypes.Output, 2003)]
  [FlowNode.Pin(2004, "装備強化画面へ移動", FlowNode.PinTypes.Output, 2004)]
  [FlowNode.Pin(2005, "マルチプレイへ移動", FlowNode.PinTypes.Output, 2005)]
  [FlowNode.Pin(2006, "イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 2006)]
  [FlowNode.Pin(2007, "アリーナへ移動", FlowNode.PinTypes.Output, 2007)]
  [FlowNode.Pin(2008, "FgGID画面へ移動", FlowNode.PinTypes.Output, 2008)]
  [FlowNode.Pin(2013, "アンナの店へ移動", FlowNode.PinTypes.Output, 2013)]
  [FlowNode.Pin(2020, "魂の交換所へ移動", FlowNode.PinTypes.Output, 2020)]
  [FlowNode.Pin(2021, "武具の店へ移動", FlowNode.PinTypes.Output, 2021)]
  [FlowNode.Pin(2025, "ユニット選択へ移動", FlowNode.PinTypes.Output, 2025)]
  [FlowNode.Pin(2026, "塔クエスト選択へ移動", FlowNode.PinTypes.Output, 2026)]
  [FlowNode.Pin(2027, "限定ショップへ移動", FlowNode.PinTypes.Output, 2027)]
  [FlowNode.Pin(2028, "イベントショップへ移動", FlowNode.PinTypes.Output, 2028)]
  [FlowNode.Pin(2045, "ポートショップへ移動", FlowNode.PinTypes.Output, 2045)]
  [FlowNode.Pin(2029, "レビューへ移動", FlowNode.PinTypes.Output, 2029)]
  [FlowNode.Pin(2030, "対戦へ移動", FlowNode.PinTypes.Output, 2030)]
  [FlowNode.Pin(2031, "マルチ塔へ移動", FlowNode.PinTypes.Output, 2031)]
  [FlowNode.Pin(2032, "キャラクエストへ移動", FlowNode.PinTypes.Output, 2032)]
  [FlowNode.Pin(2033, "初心者クエストへ移動", FlowNode.PinTypes.Output, 2033)]
  [FlowNode.Pin(2034, "初心者の館へ移動", FlowNode.PinTypes.Output, 2034)]
  [FlowNode.Pin(2035, "真理念装へ移動", FlowNode.PinTypes.Output, 2035)]
  [FlowNode.Pin(2036, "異層圏カルマへ移動", FlowNode.PinTypes.Output, 2036)]
  [FlowNode.Pin(2037, "ホーム画面へ移動", FlowNode.PinTypes.Output, 2037)]
  [FlowNode.Pin(2038, "お知らせを開く", FlowNode.PinTypes.Output, 2038)]
  [FlowNode.Pin(2039, "フレンド画面を開く", FlowNode.PinTypes.Output, 2039)]
  [FlowNode.Pin(2040, "創世編へ移動", FlowNode.PinTypes.Output, 2040)]
  [FlowNode.Pin(2041, "新イベントへ移動", FlowNode.PinTypes.Output, 2041)]
  [FlowNode.Pin(2042, "アルケミィポートへ移動", FlowNode.PinTypes.Output, 2042)]
  [FlowNode.Pin(2043, "レイドへ移動", FlowNode.PinTypes.Output, 2043)]
  [FlowNode.Pin(2044, "ポートレイドへ移動", FlowNode.PinTypes.Output, 2044)]
  public class TrophyWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INIT = 0;
    public const int PIN_REFRESH = 1;
    public const int PIN_INIT_GUILD_DAILY = 2;
    public const int PIN_REFRESH_GUILD = 3;
    public const int PIN_GETALL = 10;
    public const int PIN_GETALL_GUILD = 11;
    public const int PIN_DAILY = 1000;
    public const int PIN_RECORD = 1001;
    public const int PIN_GUILD_DAILY = 1002;
    public const int PIN_REWARD_CHECK = 2000;
    public const int PIN_GOTO_GACHA = 2001;
    public const int PIN_GOTO_QUEST = 2002;
    public const int PIN_GOTO_BUYGOLD = 2003;
    public const int PIN_GOTO_SOUBI = 2004;
    public const int PIN_GOTO_MULTI = 2005;
    public const int PIN_GOTO_EVENT = 2006;
    public const int PIN_GOTO_ARENA = 2007;
    public const int PIN_GOTO_FGGID = 2008;
    public const int PIN_GOTO_SHOP_NORMAL = 2013;
    public const int PIN_GOTO_SHOP_KAKERA = 2020;
    public const int PIN_GOTO_SHOP_ARTIFACT = 2021;
    public const int PIN_GOTO_UNIT = 2025;
    public const int PIN_GOTO_TOWER = 2026;
    public const int PIN_GOTO_SHOP_LIMITE = 2027;
    public const int PIN_GOTO_SHOP_EVENT = 2028;
    public const int PIN_GOTO_REVIEW = 2029;
    public const int PIN_GOTO_VERSUS = 2030;
    public const int PIN_GOTO_MULTITOWER = 2031;
    public const int PIN_GOTO_CHARAQUEST = 2032;
    public const int PIN_GOTO_EVENT_BEGINEER = 2033;
    public const int PIN_GOTO_BEGINNER_TOP = 2034;
    public const int PIN_GOTO_CONCEPT_CARD = 2035;
    public const int PIN_GOTO_QUEST_ORDEAL = 2036;
    public const int PIN_GOTO_HOME = 2037;
    public const int PIN_VIEW_NEWS = 2038;
    public const int PIN_GOTO_FRIEND = 2039;
    public const int PIN_GOTO_GENESIS = 2040;
    public const int PIN_GOTO_ADVANCE = 2041;
    public const int PIN_GOTO_GUILD = 2042;
    public const int PIN_GOTO_RAIDBOSS = 2043;
    public const int PIN_GOTO_GUILDRAIDBOSS = 2044;
    public const int PIN_GOTO_SHOP_PORT = 2045;
    [SerializeField]
    private GameObject daily_badge_obj;
    [SerializeField]
    private GameObject record_badge_obj;
    [SerializeField]
    private GameObject story_badge_obj;
    [SerializeField]
    private GameObject event_badge_obj;
    [SerializeField]
    private GameObject multi_badge_obj;
    [SerializeField]
    private GameObject training_badge_obj;
    [SerializeField]
    private GameObject campaign_badge_obj;
    [SerializeField]
    private GameObject other_badge_obj;
    public Toggle[] TrophyTab;
    [SerializeField]
    private GameObject DailyTrophyObj;
    [SerializeField]
    private GameObject[] RecordMissionObjs;
    [SerializeField]
    private Button BtnDailyCollectiveReceipt;
    private Dictionary<TrophyCategorys, List<TrophyCategoryData>> trophy_record_data = new Dictionary<TrophyCategorys, List<TrophyCategoryData>>();
    private Dictionary<int, TrophyCategoryData> trophy_category_data = new Dictionary<int, TrophyCategoryData>();
    private List<TrophyState> trophy_daily_data = new List<TrophyState>();
    private List<TrophyState> tmp_trophy_daily_data = new List<TrophyState>();
    private List<TrophyState> trophy_ended_data = new List<TrophyState>();
    [Space(5f)]
    [Header("ポートデイリーミッション用UI")]
    [SerializeField]
    private GameObject GuildDailyObj;
    [SerializeField]
    private Button GuildDailyCollectiveReceiptBtn;
    [SerializeField]
    private TrophyList GuildDailyTrophyList;
    private List<TrophyState> trophy_guilddaily_data = new List<TrophyState>();
    private List<TrophyState> trophy_guildended_data = new List<TrophyState>();
    private Dictionary<TrophyCategorys, List<TrophyCategoryData>> trophy_guildrecord_data = new Dictionary<TrophyCategorys, List<TrophyCategoryData>>();

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          break;
        case 1:
          this.RefreshTrophyDatas();
          TrophyList componentInChildren = ((Component) this).GetComponentInChildren<TrophyList>();
          if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
            break;
          if (componentInChildren.TrophyType == TrophyList.TrophyTypes.Normal)
          {
            ((Component) this).GetComponentInChildren<TrophyTabList>().RefreshTrophyRecord();
            break;
          }
          componentInChildren.RefreshLight();
          break;
        case 2:
          this.Initialize(2);
          break;
        case 3:
          this.RefreshGuildTrophyDatas();
          if (!Object.op_Inequality((Object) this.GuildDailyTrophyList, (Object) null))
            break;
          this.GuildDailyTrophyList.RefreshLight();
          break;
        case 10:
          this.GetAllTrophies();
          break;
        case 11:
          this.GetAllTrophiesGuild();
          break;
      }
    }

    private void GetAllTrophies() => this.GetAllTrophies(this.TrophyDailyData);

    private void GetAllTrophiesGuild() => this.GetAllTrophies(this.TrophyGuildDailyData);

    private void GetAllTrophies(List<TrophyState> dailydatas)
    {
      List<string> sel_trophy_list = new List<string>();
      RewardData reward = (RewardData) null;
      this.SetRequestTrophies(dailydatas, ref sel_trophy_list, ref reward);
      if (sel_trophy_list.Count == 0 || reward == null)
        return;
      GlobalVars.SelectedTrophy.Set((string) null);
      GlobalVars.SelectedTrophies = sel_trophy_list;
      GlobalVars.LastReward.Set(reward);
      GlobalVars.UnitGetReward = new UnitGetParam(reward.Items.ToArray());
      this.ActivateOutputLinks(2000);
    }

    private void SetRequestTrophies(
      List<TrophyState> daily_datas,
      ref List<string> sel_trophy_list,
      ref RewardData reward)
    {
      TrophyTabList componentInChildren = ((Component) this).GetComponentInChildren<TrophyTabList>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        TrophyRecordTab currentSelectionTab = componentInChildren.GetCurrentSelectionTab();
        if (Object.op_Equality((Object) currentSelectionTab, (Object) null))
          return;
        List<TrophyState> trophies = currentSelectionTab.CategoryData.Trophies;
        if (trophies == null || trophies.Count <= 0)
          return;
        foreach (TrophyState trophyState in trophies)
        {
          if (!trophyState.IsEnded && trophyState.IsCompleted && !trophyState.Param.IsInvisibleStamina() && trophyState.Param.IsAvailablePeriod(TimeManager.ServerTime, true))
          {
            sel_trophy_list.Add(trophyState.Param.iname);
            if (reward == null)
              reward = new RewardData(trophyState.Param);
            else
              reward.AddReward(trophyState.Param);
          }
        }
      }
      else
      {
        if (daily_datas == null)
          return;
        for (int index = 0; index < daily_datas.Count; ++index)
        {
          TrophyState dailyData = daily_datas[index];
          if (!dailyData.IsEnded && dailyData.IsCompleted)
          {
            sel_trophy_list.Add(dailyData.Param.iname);
            if (reward == null)
              reward = new RewardData(dailyData.Param);
            else
              reward.AddReward(dailyData.Param);
          }
        }
      }
    }

    public void ActivateOutputLinks(int pin_id)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pin_id);
    }

    private void DataClear()
    {
      this.trophy_daily_data.Clear();
      this.trophy_record_data.Clear();
      this.trophy_ended_data.Clear();
      this.trophy_guilddaily_data.Clear();
      this.trophy_guildrecord_data.Clear();
      this.trophy_guildended_data.Clear();
    }

    private void Initialize(int pinID = 0)
    {
      if (Object.op_Inequality((Object) this.DailyTrophyObj, (Object) null))
        this.DailyTrophyObj.SetActive(false);
      foreach (GameObject recordMissionObj in this.RecordMissionObjs)
      {
        if (Object.op_Inequality((Object) recordMissionObj, (Object) null))
          recordMissionObj.SetActive(false);
      }
      this.DataClear();
      if (pinID == 2)
      {
        this.RefreshGuildTrophyDatas();
        this.ActivateOutputLinks(1002);
      }
      else
      {
        this.RefreshTrophyDatas();
        bool flag1 = false;
        this.SetToggleIsOn(0);
        for (TrophyCategorys trophyCategorys = TrophyCategorys.Story; trophyCategorys <= TrophyCategorys.Other; ++trophyCategorys)
        {
          if (this.TrophyRecordData.ContainsKey(trophyCategorys))
          {
            if (!flag1)
            {
              for (int index = 0; index < this.TrophyRecordData[trophyCategorys].Count; ++index)
              {
                if (this.TrophyRecordData[trophyCategorys][index].IsInCompletedData)
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
                  flag1 = true;
                  this.SetToggleIsOn((int) this.TrophyCategorysToTabIndex(trophyCategorys));
                  break;
                }
              }
            }
            else
              break;
          }
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        bool flag2 = Object.op_Implicit((Object) instance) && instance.Player != null && instance.Player.IsCanGetRewardTrophyStarMission();
        if (this.DailyCheck(this.TrophyDailyData) || flag2)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
        }
        else
        {
          if (flag1)
            return;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
        }
      }
    }

    private bool DailyCheck(List<TrophyState> dailydatas)
    {
      for (int index = 0; index < dailydatas.Count; ++index)
      {
        if (dailydatas[index].IsCompleted && !dailydatas[index].IsEnded)
          return true;
      }
      return false;
    }

    private TrophyWindow.TabIndex TrophyCategorysToTabIndex(TrophyCategorys category)
    {
      switch (category)
      {
        case TrophyCategorys.Story:
          return TrophyWindow.TabIndex.Story;
        case TrophyCategorys.Event:
          return TrophyWindow.TabIndex.Event;
        case TrophyCategorys.Multi:
          return TrophyWindow.TabIndex.Multi;
        case TrophyCategorys.Training:
          return TrophyWindow.TabIndex.Training;
        case TrophyCategorys.Campaign:
          return TrophyWindow.TabIndex.Campaign;
        default:
          return TrophyWindow.TabIndex.Other;
      }
    }

    private void SetToggleIsOn(int index)
    {
      for (int index1 = 0; index1 < this.TrophyTab.Length; ++index1)
      {
        if (Object.op_Implicit((Object) this.TrophyTab[index1]))
          this.TrophyTab[index1].isOn = index1 == index;
      }
    }

    private void RefreshBadge()
    {
      this.story_badge_obj.SetActive(false);
      this.event_badge_obj.SetActive(false);
      this.multi_badge_obj.SetActive(false);
      this.training_badge_obj.SetActive(false);
      this.campaign_badge_obj.SetActive(false);
      this.other_badge_obj.SetActive(false);
      if (Object.op_Inequality((Object) this.daily_badge_obj, (Object) null))
      {
        this.daily_badge_obj.SetActive(false);
        for (int index = 0; index < this.trophy_daily_data.Count; ++index)
        {
          if (this.trophy_daily_data[index].IsCompleted && !this.trophy_daily_data[index].IsEnded)
          {
            this.daily_badge_obj.SetActive(true);
            break;
          }
        }
        if (!this.daily_badge_obj.activeSelf)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance) && instance.Player != null && instance.Player.IsCanGetRewardTrophyStarMission())
            this.daily_badge_obj.SetActive(true);
        }
      }
      bool flag = false;
      GameObject gameObject = (GameObject) null;
      foreach (TrophyCategorys key in this.trophy_record_data.Keys)
      {
        for (int index = 0; index < this.trophy_record_data[key].Count; ++index)
        {
          if (this.trophy_record_data[key][index].IsInCompletedData)
          {
            switch (key)
            {
              case TrophyCategorys.Story:
                gameObject = this.story_badge_obj;
                goto label_22;
              case TrophyCategorys.Event:
                gameObject = this.event_badge_obj;
                goto label_22;
              case TrophyCategorys.Multi:
                gameObject = this.multi_badge_obj;
                goto label_22;
              case TrophyCategorys.Training:
                gameObject = this.training_badge_obj;
                goto label_22;
              case TrophyCategorys.Campaign:
                gameObject = this.campaign_badge_obj;
                goto label_22;
              case TrophyCategorys.Other:
                gameObject = this.other_badge_obj;
                goto label_22;
              default:
                goto label_22;
            }
          }
        }
label_22:
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          flag = true;
          gameObject.SetActive(true);
          gameObject = (GameObject) null;
        }
      }
      this.record_badge_obj.SetActive(flag);
    }

    public Dictionary<TrophyCategorys, List<TrophyCategoryData>> TrophyRecordData
    {
      get => this.trophy_record_data;
    }

    public List<TrophyState> TrophyDailyData => this.trophy_daily_data;

    public List<TrophyState> TrophyEndedData => this.trophy_ended_data;

    private void RefreshTrophyDatas()
    {
      this.RefreshTrophyDatas(MonoSingleton<GameManager>.Instance.MasterParam.TrophyCategories, MonoSingleton<GameManager>.Instance.Trophies, (TrophyBase) MonoSingleton<GameManager>.Instance.Player.TrophyData, ref this.trophy_daily_data, ref this.trophy_ended_data, ref this.trophy_record_data);
      this.RefreshBadge();
      this.ReflectDailyCollectiveReceipt(this.BtnDailyCollectiveReceipt, this.TrophyDailyData);
    }

    private void RefreshGuildTrophyDatas()
    {
      this.RefreshTrophyDatas(MonoSingleton<GameManager>.Instance.MasterParam.GuildTrophyCategory, (TrophyParam[]) MonoSingleton<GameManager>.Instance.MasterParam.GuildTrophy, (TrophyBase) MonoSingleton<GameManager>.Instance.Player.GuildTrophyData, ref this.trophy_guilddaily_data, ref this.trophy_guildended_data, ref this.trophy_guildrecord_data);
      this.ReflectDailyCollectiveReceipt(this.GuildDailyCollectiveReceiptBtn, this.TrophyGuildDailyData);
    }

    private void RefreshTrophyDatas(
      TrophyCategoryParam[] tcp,
      TrophyParam[] trp,
      TrophyBase tb,
      ref List<TrophyState> tdd,
      ref List<TrophyState> ted,
      ref Dictionary<TrophyCategorys, List<TrophyCategoryData>> trd)
    {
      if (tcp == null || trp == null || tb == null)
        return;
      this.trophy_category_data.Clear();
      this.tmp_trophy_daily_data.Clear();
      tdd.Clear();
      ted.Clear();
      trd.Clear();
      for (int index = 0; index < tcp.Length; ++index)
      {
        if (this.IsDisplayTrophyCategory(tcp[index]) && !this.trophy_category_data.ContainsKey(tcp[index].hash_code))
          this.trophy_category_data.Add(tcp[index].hash_code, new TrophyCategoryData(tcp[index]));
      }
      for (int index = 0; index < trp.Length; ++index)
      {
        TrophyState trophyCounter = tb.GetTrophyCounter(trp[index], true);
        if (!trophyCounter.Param.is_none_category_hash && this.IsDisplayTrophy(trophyCounter))
        {
          if (trophyCounter.IsEnded)
            ted.Add(trophyCounter);
          else if (trophyCounter.Param.DispType != TrophyDispType.Hide && this.trophy_category_data.ContainsKey(trophyCounter.Param.category_hash_code))
            this.trophy_category_data[trophyCounter.Param.category_hash_code].AddTrophy(trophyCounter);
        }
      }
      foreach (int key in this.trophy_category_data.Keys)
      {
        TrophyCategoryData trophyCategoryData = this.trophy_category_data[key];
        if (trophyCategoryData.Trophies.Count > 0)
        {
          if (!trophyCategoryData.Param.IsAvailablePeriod(TimeManager.ServerTime, false))
          {
            if (trophyCategoryData.IsInCompletedData)
            {
              for (int index = trophyCategoryData.Trophies.Count - 1; index >= 0; --index)
              {
                if (!trophyCategoryData.Trophies[index].IsCompleted)
                  trophyCategoryData.RemoveTrophy(trophyCategoryData.Trophies[index]);
              }
            }
            else
              continue;
          }
          if (trophyCategoryData.Param.IsDaily)
          {
            for (int index = 0; index < trophyCategoryData.Trophies.Count; ++index)
            {
              if (trophyCategoryData.Trophies[index].IsCompleted)
                tdd.Add(trophyCategoryData.Trophies[index]);
              else
                this.tmp_trophy_daily_data.Add(trophyCategoryData.Trophies[index]);
            }
          }
          else
          {
            if (!trd.ContainsKey(trophyCategoryData.Param.category))
              trd.Add(trophyCategoryData.Param.category, new List<TrophyCategoryData>());
            trophyCategoryData.Apply();
            trd[trophyCategoryData.Param.category].Add(trophyCategoryData);
          }
        }
      }
      tdd.AddRange((IEnumerable<TrophyState>) this.tmp_trophy_daily_data);
      for (int index = 0; index < ted.Count; ++index)
      {
        TrophyState trophyState = ted[index];
        if (trophyState.Param != null && trophyState.Param.IsDaily && trophyState.Param.IsEnded)
          tdd.Add(trophyState);
      }
      this.trophy_category_data.Clear();
      this.tmp_trophy_daily_data.Clear();
    }

    private bool IsDisplayTrophyCategory(TrophyCategoryParam _tcp)
    {
      return _tcp.IsAvailablePeriod(TimeManager.ServerTime, true);
    }

    private bool IsDisplayTrophy(TrophyState st)
    {
      if (st.Param.DispType == TrophyDispType.Award || st.Param.IsChallengeMission || st.Param.IsInvisibleVip() || st.Param.IsInvisibleCard() || st.Param.IsInvisibleStamina() || st.Param.RequiredTrophies != null && !TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, st.Param, true, false) || st.Param.IsBeginner && !MonoSingleton<GameManager>.Instance.Player.IsBeginner() && !st.IsCompleted)
        return false;
      if (!st.IsCompleted && st.Param.ContainsCondition(TrophyConditionTypes.overvsrankfloor))
      {
        if (string.IsNullOrEmpty(st.Param.Objectives[0].sval_base))
        {
          if (!MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower())
            return false;
        }
        else if (!MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower(st.Param.Objectives[0].sval_base))
          return false;
      }
      return (st.Param.iname.Length < 7 || !(st.Param.iname.Substring(0, 7) == "REVIEW_") || !Network.Host.Contains("eval.alchemist.gu3.jp")) && (st.Param.IsBeginner || st.Param.CategoryParam.IsOpenLinekedQuest(TimeManager.ServerTime, st.IsCompleted)) && !st.Param.ContainsCondition(TrophyConditionTypes.fggid);
    }

    private void ReflectDailyCollectiveReceipt(Button button, List<TrophyState> dailydatas)
    {
      GameUtility.SetButtonIntaractable(button, this.DailyCheck(dailydatas));
    }

    public int GetTrophyStarMissionDailyStarCount()
    {
      int missionDailyStarCount = 0;
      for (int index = 0; index < this.trophy_daily_data.Count; ++index)
      {
        TrophyState trophyState = this.trophy_daily_data[index];
        if (trophyState != null && trophyState.Param != null)
          missionDailyStarCount += trophyState.Param.StarNum;
      }
      for (int index = 0; index < this.trophy_ended_data.Count; ++index)
      {
        TrophyState trophyState = this.trophy_ended_data[index];
        if (trophyState != null && trophyState.Param != null && trophyState.Param.IsDaily && !trophyState.Param.IsEnded)
          missionDailyStarCount += trophyState.Param.StarNum;
      }
      return missionDailyStarCount;
    }

    public List<TrophyState> TrophyGuildDailyData => this.trophy_guilddaily_data;

    public List<TrophyState> TrophyGuildEndedData => this.trophy_guildended_data;

    public Dictionary<TrophyCategorys, List<TrophyCategoryData>> TrophyGuildRecordData
    {
      get => this.trophy_guildrecord_data;
    }

    private enum TabIndex
    {
      Story,
      Event,
      Multi,
      Training,
      Campaign,
      Other,
      MAX,
    }
  }
}
