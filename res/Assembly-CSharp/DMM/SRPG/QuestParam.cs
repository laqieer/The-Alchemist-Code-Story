// Decompiled with JetBrains decompiler
// Type: SRPG.QuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestParam
  {
    private BitArray bit_array = new BitArray(19);
    private static readonly int MULTI_MAX_TOTAL_UNIT = 4;
    private static readonly int MULTI_MAX_PLAYER_UNIT = 2;
    public string iname;
    public string title;
    public string name;
    public string expr;
    private short cond_index = -1;
    private short world_index = -1;
    private short ChapterID_index = -1;
    public string mission;
    public string[] cond_quests;
    public ShareStringList units = new ShareStringList(ShareString.Type.QuestParam_units);
    public QuestDifficulties difficulty;
    public string navigation;
    public short dailyCount;
    public short dailyReset;
    public string storyTextID;
    public QuestStates state;
    public int clear_missions;
    public int[] mission_values;
    public QuestBonusObjective[] bonusObjective;
    public QuestPartyParam questParty;
    public short point;
    public short aplv;
    public short challengeLimit;
    public int pexp;
    public int uexp;
    public int gold;
    public int mcoin;
    public OShort clock_win = (OShort) 0;
    public OShort clock_lose = (OShort) 0;
    public OShort win = (OShort) 0;
    public OShort lose = (OShort) 0;
    public QuestTypes type;
    public SubQuestTypes subtype;
    public short lv;
    public OShort multi = (OShort) 0;
    public OShort multiDead = (OShort) 0;
    public OShort playerNum = (OShort) 0;
    public OShort unitNum = (OShort) 0;
    public List<MapParam> map = new List<MapParam>(BattleCore.MAX_MAP);
    public string event_start;
    public string event_clear;
    private short ticket_index = -1;
    public long start;
    public long end;
    public long key_end;
    public int key_cnt;
    public int key_limit;
    public string VersusThumnail;
    public string MapBuff;
    public int VersusMoveCount;
    public int DamageUpprPl;
    public int DamageUpprEn;
    public int DamageRatePl;
    public int DamageRateEn;
    public string MapEffectId;
    public string WeatherSetId;
    public string[] FirstClearItems;
    public int GenesisUIIndex;
    public string ResetItem;
    public int ResetMax;
    public string ResetCost;
    public string OpenUnit;
    public bool gps_enable;
    public string AllowedJobs;
    public QuestParam.Tags AllowedTags;
    public int PhysBonus;
    public int MagBonus;
    public string ItemLayout;
    public ChapterParam Chapter;
    private int[] AtkTypeMags;
    public QuestCondParam EntryCondition;
    public QuestCondParam EntryConditionCh;
    public bool IsExtra;
    private static Dictionary<string, bool> CachedGenesisIsBossLiberation;
    private static Dictionary<string, bool> CachedAdvanceIsBossLiberation;

    public string cond
    {
      set
      {
        this.cond_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.QuestParam_cond, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.QuestParam_cond, this.cond_index);
      }
    }

    public string world
    {
      set
      {
        this.world_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.QuestParam_world, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.QuestParam_world, this.world_index);
      }
    }

    public string ChapterID
    {
      set
      {
        this.ChapterID_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.QuestParam_area, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.QuestParam_area, this.ChapterID_index);
      }
    }

    public int best_clear_time { get; set; }

    public bool notSearch
    {
      set => this.bit_array.Set(1, value);
      get => this.bit_array.Get(1);
    }

    public int dayReset { get; set; }

    public bool IsMulti => (int) this.multi != 0;

    public bool IsMultiEvent => (int) this.multi >= 100;

    public bool IsMultiVersus => (int) this.multi == 2 || (int) this.multi == 102;

    public bool IsMultiAreaQuest => this.type == QuestTypes.MultiGps;

    public bool EnableRentalUnit
    {
      get
      {
        return this.type == QuestTypes.Story || this.type == QuestTypes.Free || this.type == QuestTypes.Event || this.type == QuestTypes.Character || this.type == QuestTypes.Gps || this.type == QuestTypes.StoryExtra || this.type == QuestTypes.Beginner || this.type == QuestTypes.Raid || this.type == QuestTypes.GenesisStory || this.type == QuestTypes.GenesisBoss || this.type == QuestTypes.AdvanceStory || this.type == QuestTypes.AdvanceBoss || this.type == QuestTypes.UnitRental;
      }
    }

    public string ticket
    {
      set
      {
        this.ticket_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.QuestParam_ticket, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.QuestParam_ticket, this.ticket_index);
      }
    }

    public bool AllowRetreat
    {
      set => this.bit_array.Set(5, value);
      get => this.bit_array.Get(5);
    }

    public bool AllowAutoPlay
    {
      set => this.bit_array.Set(6, value);
      get => this.bit_array.Get(6);
    }

    public bool FirstAutoPlayProhibit
    {
      set => this.bit_array.Set(16, value);
      get => this.bit_array.Get(16);
    }

    public bool Silent
    {
      set => this.bit_array.Set(7, value);
      get => this.bit_array.Get(7);
    }

    public bool DisableAbilities
    {
      set => this.bit_array.Set(8, value);
      get => this.bit_array.Get(8);
    }

    public bool DisableItems
    {
      set => this.bit_array.Set(9, value);
      get => this.bit_array.Get(9);
    }

    public bool DisableContinue
    {
      set => this.bit_array.Set(10, value);
      get => this.bit_array.Get(10);
    }

    public bool IsUnitChange
    {
      set => this.bit_array.Set(14, value);
      get => this.bit_array.Get(14);
    }

    public bool IsMultiLeaderSkill
    {
      set => this.bit_array.Set(15, value);
      get => this.bit_array.Get(15);
    }

    public bool IsWeatherNoChange
    {
      set => this.bit_array.Set(17, value);
      get => this.bit_array.Get(17);
    }

    public bool hidden
    {
      set => this.bit_array.Set(3, value);
      get => this.bit_array.Get(3);
    }

    public bool replayLimit
    {
      set => this.bit_array.Set(4, value);
      get => this.bit_array.Get(4);
    }

    public int AdvanceUIIndex
    {
      get => this.GenesisUIIndex;
      set => this.GenesisUIIndex = value;
    }

    public bool IsAutoRepeatQuestFlag
    {
      set => this.bit_array.Set(18, value);
      get => this.bit_array.Get(18);
    }

    public bool IsAutoRepeat
    {
      get
      {
        return (this.IsStoryAll || this.IsEvent || this.IsGenesisStory || this.IsAdvanceStory) && this.HasMission() && this.IsMissionCompleteALL() && this.IsAutoRepeatQuestFlag;
      }
    }

    public bool ShowReviewPopup
    {
      set => this.bit_array.Set(0, value);
      get => this.bit_array.Get(0);
    }

    public bool IsScenario => this.map.Count == 0 || string.IsNullOrEmpty(this.map[0].mapSetName);

    public bool IsStory => this.type == QuestTypes.Story;

    public bool IsStoryElite => this.type == QuestTypes.Free;

    public bool IsStoryExtra => this.type == QuestTypes.StoryExtra;

    public bool IsStoryAll => this.IsStory || this.IsStoryElite || this.IsStoryExtra;

    public bool IsEvent => this.type == QuestTypes.Event;

    public bool IsGps => this.type == QuestTypes.Gps;

    public bool IsVersus
    {
      get => this.type == QuestTypes.VersusFree || this.type == QuestTypes.VersusRank;
    }

    public bool IsRankMatch => this.type == QuestTypes.RankMatch;

    public bool IsKeyQuest => this.Chapter != null && this.Chapter.IsKeyQuest();

    public bool IsQuestDrops
    {
      get
      {
        return this.type == QuestTypes.Story || this.type == QuestTypes.Free || this.type == QuestTypes.StoryExtra || this.type == QuestTypes.Character || this.type == QuestTypes.Event || this.type == QuestTypes.Multi || this.type == QuestTypes.Gps || this.type == QuestTypes.Beginner || this.type == QuestTypes.MultiGps;
      }
    }

    public bool IsTower => this.type == QuestTypes.Tower;

    public bool IsMultiTower => this.type == QuestTypes.MultiTower;

    public bool IsRaid => this.type == QuestTypes.Raid;

    public bool IsGuildRaid => this.type == QuestTypes.GuildRaid;

    public bool IsWorldRaid => this.type == QuestTypes.WorldRaid;

    public bool IsGenesisStory => this.type == QuestTypes.GenesisStory;

    public bool IsGenesisBoss => this.type == QuestTypes.GenesisBoss;

    public bool IsGenesis => this.IsGenesisStory || this.IsGenesisBoss;

    public bool IsAdvanceStory => this.type == QuestTypes.AdvanceStory;

    public bool IsAdvanceBoss => this.type == QuestTypes.AdvanceBoss;

    public bool IsAdvance => this.IsAdvanceStory || this.IsAdvanceBoss;

    public bool IsGenAdvBoss => this.IsGenesisBoss || this.IsAdvanceBoss;

    public bool IsGvG => this.type == QuestTypes.GvG;

    public bool IsPreCalcResult => this.type == QuestTypes.Arena || this.type == QuestTypes.GvG;

    public int GainPlayerExp => this.pexp;

    public int GainUnitExp => this.uexp;

    public int OverClockTimeWin => (int) this.clock_win;

    public int OverClockTimeLose => (int) this.clock_lose;

    public bool IsBeginner
    {
      set => this.bit_array.Set(2, value);
      get => this.bit_array.Get(2);
    }

    public bool UseFixEditor
    {
      set => this.bit_array.Set(11, value);
      get => this.bit_array.Get(11);
    }

    public bool IsNoStartVoice
    {
      set => this.bit_array.Set(12, value);
      get => this.bit_array.Get(12);
    }

    public bool UseSupportUnit
    {
      set => this.bit_array.Set(13, value);
      get => this.bit_array.Get(13);
    }

    public int MissionNum => !this.HasMission() ? 0 : this.bonusObjective.Length;

    public int GetAtkTypeMag(AttackDetailTypes type)
    {
      return this.AtkTypeMags != null ? this.AtkTypeMags[(int) type] : 0;
    }

    public void SetAtkTypeMag(int[] mags) => this.AtkTypeMags = mags;

    public void Deserialize(JSON_QuestParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.name = json.name;
      this.expr = json.expr;
      this.cond = json.cond;
      this.mission = json.mission;
      this.pexp = json.pexp;
      this.uexp = json.uexp;
      this.gold = json.gold;
      this.mcoin = json.mcoin;
      this.point = CheckCast.to_short(json.pt);
      this.multi = (OShort) CheckCast.to_short(json.multi);
      this.multiDead = (OShort) CheckCast.to_short(json.multi_dead);
      this.playerNum = (OShort) CheckCast.to_short(json.pnum);
      this.unitNum = (OShort) CheckCast.to_short(json.unum <= QuestParam.MULTI_MAX_PLAYER_UNIT ? json.unum : QuestParam.MULTI_MAX_PLAYER_UNIT);
      this.aplv = CheckCast.to_short(json.aplv);
      this.challengeLimit = CheckCast.to_short(json.limit);
      this.dayReset = json.dayreset;
      if ((int) this.multi != 0)
      {
        if (json.pnum * json.unum > QuestParam.MULTI_MAX_TOTAL_UNIT)
          DebugUtility.LogError("iname:" + json.iname + " / Current total unit is " + (object) (json.pnum * json.unum) + ". Please set the total number of units to" + (object) QuestParam.MULTI_MAX_TOTAL_UNIT);
        if (json.unum > QuestParam.MULTI_MAX_PLAYER_UNIT)
          DebugUtility.LogError("iname:" + json.iname + " / Current 1 player unit is " + (object) json.unum + ". Please set the 1 player number of units to" + (object) QuestParam.MULTI_MAX_PLAYER_UNIT);
      }
      this.key_limit = json.key_limit;
      this.clock_win = (OShort) CheckCast.to_short(json.ctw);
      this.clock_lose = (OShort) CheckCast.to_short(json.ctl);
      this.lv = CheckCast.to_short(Math.Max(json.lv, 1));
      this.win = (OShort) CheckCast.to_short(json.win);
      this.lose = (OShort) CheckCast.to_short(json.lose);
      this.type = (QuestTypes) json.type;
      this.subtype = (SubQuestTypes) json.subtype;
      this.cond_quests = (string[]) null;
      this.units.Clear();
      this.ChapterID = json.area;
      this.world = json.world;
      this.storyTextID = json.text;
      this.hidden = json.hide != 0;
      this.replayLimit = json.replay_limit != 0;
      this.ticket = json.ticket;
      this.title = json.title;
      this.navigation = json.nav;
      this.AllowedJobs = json.ajob;
      this.AllowedTags = (QuestParam.Tags) 0;
      if (!string.IsNullOrEmpty(json.atag))
      {
        string[] strArray = json.atag.Split(',');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray[index]))
            this.AllowedTags |= (QuestParam.Tags) Enum.Parse(typeof (QuestParam.Tags), strArray[index]);
        }
      }
      this.PhysBonus = json.phyb + 100;
      this.MagBonus = json.magb + 100;
      this.IsBeginner = 0 != json.bgnr;
      this.ItemLayout = json.i_lyt;
      this.notSearch = json.not_search != 0;
      ObjectiveParam objective = MonoSingleton<GameManager>.GetInstanceDirect().FindObjective(json.mission);
      if (objective != null)
      {
        this.bonusObjective = new QuestBonusObjective[objective.objective.Length];
        for (int index = 0; index < objective.objective.Length; ++index)
        {
          this.bonusObjective[index] = new QuestBonusObjective();
          this.bonusObjective[index].Type = (EMissionType) objective.objective[index].type;
          this.bonusObjective[index].TypeParam = objective.objective[index].val;
          this.bonusObjective[index].item = objective.objective[index].item;
          this.bonusObjective[index].itemNum = objective.objective[index].num;
          this.bonusObjective[index].itemType = (RewardType) objective.objective[index].item_type;
          this.bonusObjective[index].IsTakeoverProgress = objective.objective[index].IsTakeoverProgress;
        }
      }
      ObjectiveParam towerObjective = MonoSingleton<GameManager>.GetInstanceDirect().FindTowerObjective(json.tower_mission);
      if (towerObjective != null)
      {
        this.bonusObjective = new QuestBonusObjective[towerObjective.objective.Length];
        for (int index = 0; index < towerObjective.objective.Length; ++index)
        {
          this.bonusObjective[index] = new QuestBonusObjective();
          this.bonusObjective[index].Type = (EMissionType) towerObjective.objective[index].type;
          this.bonusObjective[index].TypeParam = towerObjective.objective[index].val;
          this.bonusObjective[index].item = towerObjective.objective[index].item;
          this.bonusObjective[index].itemNum = towerObjective.objective[index].num;
          this.bonusObjective[index].itemType = (RewardType) towerObjective.objective[index].item_type;
          this.bonusObjective[index].IsTakeoverProgress = towerObjective.objective[index].IsTakeoverProgress;
        }
        this.mission_values = new int[towerObjective.objective.Length];
      }
      MagnificationParam magnification = MonoSingleton<GameManager>.GetInstanceDirect().FindMagnification(json.atk_mag);
      if (magnification != null && magnification.atkMagnifications != null)
        this.AtkTypeMags = magnification.atkMagnifications;
      QuestCondParam questCond1 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd);
      if (questCond1 != null)
        this.EntryCondition = questCond1;
      QuestCondParam questCond2 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd_ch);
      if (questCond2 != null)
        this.EntryConditionCh = questCond2;
      this.difficulty = (QuestDifficulties) json.mode;
      if (json.units != null)
      {
        this.units.Setup(json.units.Length);
        for (int index = 0; index < json.units.Length; ++index)
          this.units.Set(index, json.units[index]);
      }
      if (json.cond_quests != null)
      {
        this.cond_quests = new string[json.cond_quests.Length];
        for (int index = 0; index < json.cond_quests.Length; ++index)
          this.cond_quests[index] = json.cond_quests[index];
      }
      this.map.Clear();
      if (json.map != null)
      {
        for (int index = 0; index < json.map.Length; ++index)
        {
          MapParam mapParam = new MapParam();
          mapParam.Deserialize(json.map[index]);
          this.map.Add(mapParam);
        }
      }
      this.event_start = json.evst;
      this.event_clear = json.evw;
      this.AllowRetreat = json.retr == 0;
      this.AllowAutoPlay = json.naut == 0 || json.naut == 2;
      this.FirstAutoPlayProhibit = json.naut == 2;
      this.Silent = json.swin != 0;
      this.DisableAbilities = json.notabl != 0;
      this.DisableItems = json.notitm != 0;
      this.DisableContinue = json.notcon != 0;
      this.UseFixEditor = json.fix_editor != 0;
      this.IsNoStartVoice = json.is_no_start_voice != 0;
      this.UseSupportUnit = json.sprt == 0;
      this.IsUnitChange = json.is_unit_chg != 0;
      this.VersusThumnail = json.thumnail;
      this.MapBuff = json.mskill;
      this.VersusMoveCount = json.vsmovecnt;
      this.DamageUpprPl = json.dmg_up_pl;
      this.DamageUpprEn = json.dmg_up_en;
      this.DamageRatePl = json.dmg_rt_pl;
      this.DamageRateEn = json.dmg_rt_en;
      this.IsExtra = json.extra == 1;
      this.ShowReviewPopup = json.review == 1;
      this.IsMultiLeaderSkill = json.is_multileader != 0;
      this.MapEffectId = json.me_id;
      this.IsWeatherNoChange = json.is_wth_no_chg != 0;
      this.WeatherSetId = json.wth_set_id;
      if (json.fclr_items != null)
      {
        this.FirstClearItems = new string[json.fclr_items.Length];
        for (int index = 0; index < json.fclr_items.Length; ++index)
          this.FirstClearItems[index] = json.fclr_items[index];
      }
      this.questParty = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestParty(json.party_id);
      this.GenesisUIIndex = json.gen_ui_index;
      this.ResetItem = json.reset_item;
      this.ResetMax = json.reset_max;
      this.ResetCost = json.reset_cost;
      this.OpenUnit = json.open_unit;
      this.IsAutoRepeatQuestFlag = json.is_auto_repeat_quest != 0;
    }

    public bool IsUnitAllowed(UnitData unit)
    {
      if (unit == null)
        return true;
      if (!string.IsNullOrEmpty(this.AllowedJobs) && unit.CurrentJob != null && unit.CurrentJob.Param != null)
      {
        string iname = unit.CurrentJob.Param.iname;
        int length = iname.Length;
        int num = this.AllowedJobs.IndexOf(iname);
        if (num < 0 || 0 < num && this.AllowedJobs[num - 1] != ',' || num + length < this.AllowedJobs.Length && this.AllowedJobs[num + length - 1] != ',')
          return false;
      }
      return this.AllowedTags == (QuestParam.Tags) 0 || ((this.AllowedTags & QuestParam.Tags.MAL) == (QuestParam.Tags) 0 || unit.UnitParam.sex == ESex.Male) && ((this.AllowedTags & QuestParam.Tags.FEM) == (QuestParam.Tags) 0 || unit.UnitParam.sex == ESex.Female) && ((this.AllowedTags & QuestParam.Tags.HERO) == (QuestParam.Tags) 0 || unit.UnitParam.IsHero());
    }

    public bool IsQuestCondition()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.IsBeginner() && this.IsBeginner)
        return false;
      if (this.cond_quests != null)
      {
        for (int index = 0; index < this.cond_quests.Length; ++index)
        {
          QuestParam quest = instance.FindQuest(this.cond_quests[index]);
          if (quest != null && quest.state != QuestStates.Cleared)
            return false;
        }
      }
      return true;
    }

    public List<QuestParam> DetectNotClearConditionQuests()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.IsBeginner() && this.IsBeginner)
        return (List<QuestParam>) null;
      if (this.cond_quests == null)
        return (List<QuestParam>) null;
      List<QuestParam> questParamList = new List<QuestParam>();
      for (int index = 0; index < this.cond_quests.Length; ++index)
      {
        QuestParam quest = instance.FindQuest(this.cond_quests[index]);
        if (quest != null && quest.state != QuestStates.Cleared)
          questParamList.Add(quest);
      }
      return questParamList;
    }

    public bool IsAvailableUnit(UnitData unit)
    {
      return this.IsAvailableUnitInternal(this.EntryCondition, unit);
    }

    public bool IsAvailableUnitCh(UnitData unit)
    {
      return this.IsAvailableUnitInternal(this.EntryConditionCh, unit);
    }

    public bool IsAvailableUnitInternal(QuestCondParam condition, UnitData unit)
    {
      if (condition == null || condition.unit == null || condition.unit.Length <= 0)
        return true;
      foreach (string str in condition.unit)
      {
        if (unit.UnitID == str)
          return true;
      }
      return false;
    }

    public bool IsEntryQuestCondition(IEnumerable<UnitData> entryUnits, ref string error)
    {
      error = string.Empty;
      if (this.EntryCondition != null)
      {
        int num = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
        if (this.EntryCondition.plvmax > 0 && num > this.EntryCondition.plvmax)
        {
          error = "sys.PARTYEDITOR_PLV";
          return false;
        }
        if (this.EntryCondition.plvmin > 0 && num < this.EntryCondition.plvmin)
        {
          error = "sys.PARTYEDITOR_PLV";
          return false;
        }
      }
      foreach (UnitData entryUnit in entryUnits)
      {
        if (entryUnit != null && !this.IsEntryQuestCondition(entryUnit, ref error))
          return false;
      }
      return true;
    }

    public bool IsEntryQuestCondition(UnitData unit)
    {
      string error = (string) null;
      return this.IsEntryQuestCondition(this.EntryCondition, unit, ref error);
    }

    public bool IsEntryQuestCondition(UnitData unit, ref string error)
    {
      return this.IsEntryQuestCondition(this.EntryCondition, unit, ref error);
    }

    public bool IsEntryQuestConditionCh(UnitData unit, ref string error)
    {
      return this.IsEntryQuestCondition(this.EntryConditionCh, unit, ref error);
    }

    private bool IsEntryQuestCondition(QuestCondParam condition, UnitData unit, ref string error)
    {
      error = string.Empty;
      if (unit == null)
        return false;
      if (this.IsGuildRaid)
      {
        if (Network.Mode == Network.EConnectMode.Offline)
          return true;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("GuildRaidManagerが存在しない");
          return false;
        }
        GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildRaidManager.Instance.PeriodId);
        if (guildRaidPeriodParam == null || unit.CalcLevel() < guildRaidPeriodParam.UnitLevelMin)
        {
          error = "sys.PARTYEDITOR_ULV";
          return false;
        }
        if (GuildRaidManager.Instance.BattleType == GuildRaidBattleType.Main && GuildRaidManager.Instance.UsedUnitInameList != null && GuildRaidManager.Instance.UsedUnitInameList.Contains(unit.UnitID) && !GuildRaidManager.Instance.IsForcedDeck)
        {
          error = "sys.GUILDRAID_PARTY_ERROR_USED_UNIT";
          return false;
        }
      }
      if (this.IsGvG && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.UsedUnitList != null)
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGBattleTop.Instance, (UnityEngine.Object) null) && GvGBattleTop.Instance.SelfParty.WinNum > 0 || !GvGManager.Instance.UsedUnitList.Contains(unit.UniqueID);
      if (this.IsWorldRaid && UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidBossManager.Instance, (UnityEngine.Object) null) && WorldRaidBossManager.GetCurrentBossUsedUnitInameList().Contains(unit.UnitID))
      {
        error = "sys.WORLDRAID_ERROR_USED_UNIT";
        return false;
      }
      if (condition == null)
      {
        if (this.type == QuestTypes.Tower)
          DebugUtility.LogError("塔 コンディションが入っていません iname = " + this.iname);
        return true;
      }
      if (condition.unit != null && condition.unit.Length > 0 && Array.IndexOf<string>(condition.unit, unit.UnitID) == -1)
      {
        error = "sys.PARTYEDITOR_UNIT";
        return false;
      }
      if (condition.sex != ESex.Unknown && condition.sex != unit.UnitParam.sex)
      {
        error = "sys.PARTYEDITOR_SEX";
        return false;
      }
      int num1 = Math.Max(condition.rmax - 1, 0);
      if (condition.rmax > 0 && unit.Rarity > num1)
      {
        error = "sys.PARTYEDITOR_RARITY";
        return false;
      }
      int num2 = Math.Max(condition.rmin - 1, 0);
      if (condition.rmin > 0 && unit.Rarity < num2)
      {
        error = "sys.PARTYEDITOR_RARITY";
        return false;
      }
      int num3 = Math.Max(condition.rmax_ini - 1, 0);
      if (condition.rmax_ini > 0 && (int) unit.UnitParam.rare > num3)
      {
        error = "sys.PARTYEDITOR_RARITY_INI";
        return false;
      }
      int num4 = Math.Max(condition.rmin_ini - 1, 0);
      if (condition.rmin_ini > 0 && (int) unit.UnitParam.rare < num4)
      {
        error = "sys.PARTYEDITOR_RARITY_INI";
        return false;
      }
      if (condition.hmax > 0 && (unit.UnitParam.height == (short) 0 || (int) unit.UnitParam.height > condition.hmax))
      {
        error = "sys.PARTYEDITOR_HEIGHT";
        return false;
      }
      if (condition.hmin > 0 && (unit.UnitParam.height == (short) 0 || (int) unit.UnitParam.height < condition.hmin))
      {
        error = "sys.PARTYEDITOR_HEIGHT";
        return false;
      }
      if (condition.wmax > 0 && (unit.UnitParam.weight == (short) 0 || (int) unit.UnitParam.weight > condition.wmax))
      {
        error = "sys.PARTYEDITOR_WEIGHT";
        return false;
      }
      if (condition.wmin > 0 && (unit.UnitParam.weight == (short) 0 || (int) unit.UnitParam.weight < condition.wmin))
      {
        error = "sys.PARTYEDITOR_WEIGHT";
        return false;
      }
      if (condition.jobset != null && condition.jobset.Length > 0 && Array.IndexOf<int>(condition.jobset, 1) != -1)
      {
        int jobIndex = unit.JobIndex;
        if (jobIndex < 0 || jobIndex >= condition.jobset.Length || condition.jobset[jobIndex] == 0)
        {
          error = "sys.PARTYEDITOR_JOBINDEX";
          return false;
        }
      }
      if (condition.birth != null && condition.birth.Length > 0 && Array.IndexOf<string>(condition.birth, (string) unit.UnitParam.birth) == -1)
      {
        error = "sys.PARTYEDITOR_BIRTH";
        return false;
      }
      if (condition.isElemLimit && condition.elem[(int) unit.Element] == 0)
      {
        error = "sys.PARTYEDITOR_ELEM";
        return false;
      }
      if (condition.job != null)
      {
        JobData currentJob = unit.CurrentJob;
        if (currentJob == null || currentJob.Param == null)
        {
          error = "sys.PARTYEDITOR_JOB";
          return false;
        }
        if (Array.IndexOf<string>(condition.job, currentJob.JobID) == -1)
        {
          if (string.IsNullOrEmpty(currentJob.Param.origin))
          {
            error = "sys.PARTYEDITOR_JOB";
            return false;
          }
          JobParam jobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(currentJob.Param.origin);
          if (jobParam == null || Array.IndexOf<string>(condition.job, jobParam.iname) == -1)
          {
            error = "sys.PARTYEDITOR_JOB";
            return false;
          }
        }
      }
      int num5 = unit.CalcLevel();
      if (condition.ulvmax > 0 && num5 > condition.ulvmax)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (condition.ulvmin > 0 && num5 < condition.ulvmin)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (this.type != QuestTypes.Tower || !MonoSingleton<GameManager>.Instance.TowerResuponse.IsDied_PlayerUnit(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UniqueID)))
        return true;
      error = "sys.ERROR_TOWER_DEAD_UNIT";
      return false;
    }

    public List<string> GetEntryQuestConditions(bool titled = true, bool includeUnitLv = true, bool includeUnits = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryCondition, titled, includeUnitLv, includeUnits);
    }

    public List<string> GetEntryQuestConditionsCh(
      bool titled = true,
      bool includeUnitLv = true,
      bool includeUnits = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryConditionCh, titled, includeUnitLv, includeUnits);
    }

    private List<string> GetEntryQuestConditionsInternal(
      QuestCondParam condParam,
      bool titled = true,
      bool includeUnitLv = true,
      bool includeUnits = true)
    {
      List<string> conditionsInternal = new List<string>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (condParam != null && condParam.is_not_solo)
      {
        string str = LocalizedText.Get("sys.PARTYEDITOR_COND_NOT_SORO");
        conditionsInternal.Add(str);
      }
      if (condParam != null && (condParam.plvmin > 0 || condParam.plvmax > 0))
      {
        int playerLevelCap = instanceDirect.MasterParam.GetPlayerLevelCap();
        int num1 = Math.Max(condParam.plvmin, 1);
        int num2 = Math.Min(condParam.plvmax, playerLevelCap);
        string str1 = string.Empty;
        string str2;
        if (titled)
        {
          string str3 = LocalizedText.Get("sys.PARTYEDITOR_COND_PLV");
          if (num1 > 0)
            str3 += (string) (object) num1;
          str2 = str3 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 += (string) (object) num2;
        }
        else
        {
          if (num1 > 0)
            str1 = str1 + LocalizedText.Get("sys.PLV") + (object) num1;
          str2 = str1 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 = str2 + LocalizedText.Get("sys.PLV") + (object) num2;
        }
        conditionsInternal.Add(str2);
      }
      if (condParam != null && includeUnitLv && (condParam.ulvmin > 0 || condParam.ulvmax > 0))
      {
        int unitMaxLevel = instanceDirect.MasterParam.GetUnitMaxLevel();
        int num3 = Math.Max(condParam.ulvmin, 1);
        int num4 = Math.Min(condParam.ulvmax, unitMaxLevel);
        string str4 = string.Empty;
        string str5;
        if (titled)
        {
          string str6 = LocalizedText.Get("sys.PARTYEDITOR_COND_ULV");
          if (num3 > 0)
            str6 += (string) (object) num3;
          str5 = str6 + LocalizedText.Get("sys.TILDE");
          if (num4 > 0)
            str5 += (string) (object) num4;
        }
        else
        {
          if (num3 > 0)
            str4 = str4 + LocalizedText.Get("sys.ULV") + (object) num3;
          str5 = str4 + LocalizedText.Get("sys.TILDE");
          if (num4 > 0)
            str5 = str5 + LocalizedText.Get("sys.ULV") + (object) num4;
        }
        conditionsInternal.Add(str5);
      }
      if (includeUnits)
      {
        string[] strArray = condParam?.unit;
        if (condParam != null && condParam.party_type == PartyCondType.Limited)
          strArray = condParam.unit;
        else if (this.questParty != null)
          strArray = !((IEnumerable<PartySlotTypeUnitPair>) this.questParty.GetMainSubSlots()).Any<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.Free)) ? ((IEnumerable<PartySlotTypeUnitPair>) this.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.Forced || slot.Type == PartySlotType.ForcedHero || slot.Type == PartySlotType.Npc || slot.Type == PartySlotType.NpcHero)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>() : (string[]) null;
        else if (condParam != null && condParam.party_type == PartyCondType.Forced)
          strArray = condParam.unit;
        if (strArray != null && strArray.Length > 0)
        {
          List<string> stringList = new List<string>();
          for (int index = 0; index < strArray.Length; ++index)
          {
            UnitParam unitParam = instanceDirect.GetUnitParam(strArray[index]);
            if (unitParam != null && !stringList.Contains(unitParam.name))
              stringList.Add(unitParam.name);
          }
          if (stringList.Count > 0)
          {
            string str7 = string.Empty;
            for (int index = 0; index < stringList.Count; ++index)
              str7 = str7 + (index <= 0 ? string.Empty : "、") + stringList[index];
            if (!string.IsNullOrEmpty(str7))
            {
              string empty = string.Empty;
              if (titled)
                empty = LocalizedText.Get("sys.PARTYEDITOR_COND_UNIT");
              string str8 = empty + str7;
              conditionsInternal.Add(str8);
            }
          }
        }
      }
      if (condParam != null && condParam.job != null && condParam.job.Length > 0)
      {
        List<string> stringList = new List<string>();
        for (int index = 0; index < condParam.job.Length; ++index)
        {
          JobParam jobParam = instanceDirect.GetJobParam(condParam.job[index]);
          if (jobParam != null && !stringList.Contains(jobParam.name))
            stringList.Add(jobParam.name);
        }
        if (stringList.Count > 0)
        {
          string str9 = string.Empty;
          for (int index = 0; index < stringList.Count; ++index)
            str9 = str9 + (index <= 0 ? string.Empty : "、") + stringList[index];
          if (!string.IsNullOrEmpty(str9))
          {
            string empty = string.Empty;
            if (titled)
              empty = LocalizedText.Get("sys.PARTYEDITOR_COND_JOB");
            string str10 = empty + str9;
            conditionsInternal.Add(str10);
          }
        }
      }
      if (condParam != null && condParam.jobset != null && condParam.jobset.Length > 0 && Array.IndexOf<int>(condParam.jobset, 1) != -1)
      {
        string str11 = string.Empty;
        int index = 0;
        int num5 = 0;
        for (; index < condParam.jobset.Length; ++index)
        {
          if (condParam.jobset[index] != 0)
          {
            int num6 = index + 1;
            str11 = str11 + (num5 <= 0 ? (object) string.Empty : (object) "、") + (object) num6;
            ++num5;
          }
        }
        if (!string.IsNullOrEmpty(str11))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX");
          string str12 = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX_VALUE", (object) str11);
          conditionsInternal.Add(str12);
        }
      }
      if (condParam != null && (condParam.rmin > 0 || condParam.rmax > 0))
      {
        string empty = string.Empty;
        if (titled)
          empty = LocalizedText.Get("sys.PARTYEDITOR_COND_RARITY");
        int num7 = Math.Max(condParam.rmin - 1, 0);
        int num8 = Math.Max(condParam.rmax - 1, 0);
        string str;
        if (num7 == num8)
        {
          str = empty + LocalizedText.Get("sys.RARITY_STAR_" + (object) num7);
        }
        else
        {
          if (num7 >= 0)
            empty += LocalizedText.Get("sys.RARITY_STAR_" + (object) num7);
          str = empty + LocalizedText.Get("sys.TILDE");
          if (num8 >= 0)
            str += LocalizedText.Get("sys.RARITY_STAR_" + (object) num8);
        }
        conditionsInternal.Add(str);
      }
      if (condParam != null && (condParam.rmin_ini > 0 || condParam.rmax_ini > 0))
      {
        string empty = string.Empty;
        if (titled)
          empty = LocalizedText.Get("sys.PARTYEDITOR_COND_RARITY_INI");
        int num9 = Math.Max(condParam.rmin_ini - 1, 0);
        int num10 = Math.Max(condParam.rmax_ini - 1, 0);
        string str;
        if (num9 == num10)
        {
          str = empty + LocalizedText.Get("sys.RARITY_STAR_" + (object) num9);
        }
        else
        {
          if (num9 >= 0)
            empty += LocalizedText.Get("sys.RARITY_STAR_" + (object) num9);
          str = empty + LocalizedText.Get("sys.TILDE");
          if (num10 >= 0)
            str += LocalizedText.Get("sys.RARITY_STAR_" + (object) num10);
        }
        conditionsInternal.Add(str);
      }
      if (condParam != null && condParam.isElemLimit)
      {
        string str13 = string.Empty;
        int index = 0;
        int num = 0;
        for (; index < condParam.elem.Length; ++index)
        {
          if (index != 0 && condParam.elem[index] != 0)
          {
            str13 = str13 + (num <= 0 ? string.Empty : "、") + LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) index);
            ++num;
          }
        }
        if (!string.IsNullOrEmpty(str13))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_ELEM");
          string str14 = empty + str13;
          conditionsInternal.Add(str14);
        }
      }
      if (condParam != null && condParam.birth != null && condParam.birth.Length > 0)
      {
        string str15 = string.Empty;
        for (int index = 0; index < condParam.birth.Length; ++index)
          str15 = str15 + (index <= 0 ? string.Empty : "、") + condParam.birth[index];
        if (!string.IsNullOrEmpty(str15))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_BIRTH");
          string str16 = empty + str15;
          conditionsInternal.Add(str16);
        }
      }
      if (condParam != null && condParam.sex != ESex.Unknown)
      {
        string str17 = LocalizedText.Get("sys.SEX_" + (object) (int) condParam.sex);
        if (!string.IsNullOrEmpty(str17))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_SEX");
          string str18 = empty + str17;
          conditionsInternal.Add(str18);
        }
      }
      if (condParam != null && (condParam.hmin > 0 || condParam.hmax > 0))
      {
        int hmin = condParam.hmin;
        int hmax = condParam.hmax;
        string str19 = string.Empty;
        if (titled)
          str19 = LocalizedText.Get("sys.PARTYEDITOR_COND_HEIGHT");
        if (hmin > 0)
          str19 = str19 + (object) hmin + LocalizedText.Get("sys.CM_HEIGHT");
        string str20 = str19 + LocalizedText.Get("sys.TILDE");
        if (hmax > 0)
          str20 = str20 + (object) hmax + LocalizedText.Get("sys.CM_HEIGHT");
        conditionsInternal.Add(str20);
      }
      if (condParam != null && (condParam.wmin > 0 || condParam.wmax > 0))
      {
        int wmin = condParam.wmin;
        int wmax = condParam.wmax;
        string str21 = string.Empty;
        if (titled)
          str21 = LocalizedText.Get("sys.PARTYEDITOR_COND_WEIGHT");
        if (wmin > 0)
          str21 = str21 + (object) wmin + LocalizedText.Get("sys.KG_WEIGHT");
        string str22 = str21 + LocalizedText.Get("sys.TILDE");
        if (wmax > 0)
          str22 = str22 + (object) wmax + LocalizedText.Get("sys.KG_WEIGHT");
        conditionsInternal.Add(str22);
      }
      return conditionsInternal;
    }

    public List<string> GetAddQuestInfo(bool is_inc_title = true)
    {
      List<string> addQuestInfo = new List<string>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return addQuestInfo;
      if (!string.IsNullOrEmpty(this.MapEffectId))
      {
        MapEffectParam mapEffectParam = instanceDirect.GetMapEffectParam(this.MapEffectId);
        if (mapEffectParam != null)
        {
          string empty = string.Empty;
          if (is_inc_title)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT");
          string str = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_HEAD") + mapEffectParam.Name + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_BOTTOM");
          addQuestInfo.Add(str);
        }
      }
      if (!string.IsNullOrEmpty(this.WeatherSetId))
      {
        WeatherSetParam weatherSetParam = instanceDirect.GetWeatherSetParam(this.WeatherSetId);
        if (weatherSetParam != null)
        {
          List<string> wth_name_lists = new List<string>();
          this.AddWeatherNameLists(wth_name_lists, weatherSetParam.StartWeatherIdLists);
          this.AddWeatherNameLists(wth_name_lists, weatherSetParam.ChangeWeatherIdLists);
          if (wth_name_lists.Count != 0)
          {
            string empty = string.Empty;
            if (is_inc_title)
              empty = LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER");
            string str1 = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_HEAD");
            for (int index = 0; index < wth_name_lists.Count; ++index)
            {
              if (index != 0)
                str1 += LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_SEP");
              str1 += wth_name_lists[index];
            }
            string str2 = str1 + LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_BOTTOM");
            addQuestInfo.Add(str2);
          }
        }
      }
      return addQuestInfo;
    }

    private void AddWeatherNameLists(List<string> wth_name_lists, List<string> wth_id_lists)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || wth_id_lists == null)
        return;
      foreach (string wthIdList in wth_id_lists)
      {
        WeatherParam weatherParam = instanceDirect.MasterParam.GetWeatherParam(wthIdList);
        if (weatherParam != null && !wth_name_lists.Contains(weatherParam.Name))
          wth_name_lists.Add(weatherParam.Name);
      }
    }

    public bool IsJigen => this.end != 0L;

    public bool IsDateUnlock(long serverTime = -1)
    {
      if (this.IsGenesis)
      {
        GenesisParam genesisParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGenesisParam();
        return genesisParam != null && genesisParam.IsWithinPeriod();
      }
      long num = serverTime >= 0L ? serverTime : Network.GetServerTime();
      if (!MonoSingleton<GameManager>.Instance.Player.IsBeginner() && this.IsBeginner)
        return false;
      if (!this.IsJigen)
        return !this.hidden;
      return this.start <= num && num < this.end;
    }

    public bool IsReplayDateUnlock(long serverTime = -1)
    {
      return !this.replayLimit || this.IsDateUnlock(serverTime);
    }

    public int GetSelectMainMemberNum()
    {
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      int selectMainMemberNum = 0;
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Tutorial:
        case QuestTypes.Character:
        case QuestTypes.MultiGps:
          selectMainMemberNum = party.MAX_MAINMEMBER - 1;
          break;
        case QuestTypes.Arena:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
        case QuestTypes.UnitRental:
        case QuestTypes.GuildRaid:
        case QuestTypes.GvG:
        case QuestTypes.WorldRaid:
          selectMainMemberNum = party.MAX_MAINMEMBER;
          break;
      }
      return selectMainMemberNum;
    }

    public bool CheckEnableEntrySubMembers()
    {
      QuestTypes type = this.type;
      switch (type)
      {
        case QuestTypes.Story:
        case QuestTypes.Tutorial:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
        case QuestTypes.Raid:
          return true;
        default:
          if (type != QuestTypes.GuildRaid)
            return false;
          goto case QuestTypes.Story;
      }
    }

    public bool CheckAllowedAutoBattle()
    {
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Tutorial:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.RankMatch:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
        case QuestTypes.UnitRental:
        case QuestTypes.GuildRaid:
        case QuestTypes.WorldRaid:
          return (!this.FirstAutoPlayProhibit || this.state == QuestStates.Cleared) && this.AllowAutoPlay;
        case QuestTypes.Multi:
        case QuestTypes.MultiGps:
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            return false;
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null || currentRoom.json == null)
            return false;
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          return this.AllowAutoPlay && myPhotonRoomParam != null && myPhotonRoomParam.autoAllowed != 0;
        default:
          return false;
      }
    }

    public bool CheckAllowedRetreat() => this.type != QuestTypes.Tutorial && this.AllowRetreat;

    public bool CheckDisableAbilities() => this.DisableAbilities;

    public bool CheckDisableItems() => this.DisableItems;

    public bool CheckDisableContinue() => this.DisableContinue;

    public bool CheckEnableQuestResult()
    {
      return this.type != QuestTypes.Tutorial && !this.IsPreCalcResult;
    }

    public bool CheckEnableGainedItem() => this.type != QuestTypes.Tutorial;

    public bool CheckEnableGainedGold() => this.type != QuestTypes.Tutorial;

    public bool CheckEnableGainedExp() => this.type != QuestTypes.Tutorial;

    public bool CheckEnableSuspendStart()
    {
      return this.type != QuestTypes.Tutorial && this.type != QuestTypes.Multi && this.type != QuestTypes.MultiGps && !this.IsPreCalcResult;
    }

    public void SetChallangeCount(int count)
    {
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        this.key_cnt = count;
      else
        this.dailyCount = CheckCast.to_short(count);
    }

    public int GetChallangeCount()
    {
      return this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count ? this.key_cnt : (int) this.dailyCount;
    }

    public int GetChapterChallangeCount()
    {
      if (this.Chapter != null)
      {
        ChapterParam chapter;
        this.Chapter.CheckEnableChallange(out chapter);
        if (chapter != null && chapter.HasChallengeLimit)
          return chapter.challengeCount;
      }
      return 0;
    }

    public int GetChallangeLimit()
    {
      return this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count ? this.key_limit : MonoSingleton<GameManager>.Instance.Player.GetChallengeLimitCount(ExpansionPurchaseParam.eExpansionType.ChallengeCount, this.iname, (int) this.challengeLimit);
    }

    public int GetChapterChallangeLimit()
    {
      if (this.Chapter != null)
      {
        ChapterParam chapter;
        this.Chapter.CheckEnableChallange(out chapter);
        if (chapter != null && chapter.HasChallengeLimit)
          return chapter.ChallengeLimitCount();
      }
      return 0;
    }

    public bool CheckEnableChallange()
    {
      int challangeLimit = this.GetChallangeLimit();
      return (challangeLimit <= 0 || this.GetChallangeCount() < challangeLimit) && (this.Chapter == null || this.Chapter.CheckEnableChallange());
    }

    public bool CheckEnableReset() => this.ResetMax > (int) this.dailyReset;

    public int GetRestResetCount() => Math.Max(0, this.ResetMax - (int) this.dailyReset);

    public int RequiredApWithPlayerLv(int playerLv, bool campaign = true)
    {
      if (playerLv < (int) this.aplv)
        return 0;
      int point = (int) this.point;
      if (campaign)
      {
        foreach (QuestCampaignData questCampaign in MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this))
        {
          if (questCampaign.type == QuestCampaignValueTypes.Ap)
          {
            point = Mathf.FloorToInt((float) point * questCampaign.GetRate());
            break;
          }
        }
      }
      return point;
    }

    public static QuestTypes ToQuestType(string name)
    {
      if (name != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (QuestParam.\u003C\u003Ef__switch\u0024map3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          QuestParam.\u003C\u003Ef__switch\u0024map3 = new Dictionary<string, int>(22)
          {
            {
              "Story",
              0
            },
            {
              "multi",
              1
            },
            {
              "Arena",
              2
            },
            {
              "Tutorial",
              3
            },
            {
              "Free",
              4
            },
            {
              "extra",
              5
            },
            {
              "Event",
              6
            },
            {
              "Character",
              7
            },
            {
              "tower",
              8
            },
            {
              "vs",
              9
            },
            {
              "vs_tower",
              9
            },
            {
              "rm",
              10
            },
            {
              "multi_tower",
              11
            },
            {
              "multi_areaquest",
              12
            },
            {
              "raidboss",
              13
            },
            {
              "guildraid",
              14
            },
            {
              "genesis",
              15
            },
            {
              "genesis_raid",
              16
            },
            {
              "advance",
              17
            },
            {
              "advance_raid",
              18
            },
            {
              "gvg",
              19
            },
            {
              "worldraid",
              20
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (QuestParam.\u003C\u003Ef__switch\u0024map3.TryGetValue(name, out num))
        {
          switch (num)
          {
            case 0:
              return QuestTypes.Story;
            case 1:
              return QuestTypes.Multi;
            case 2:
              return QuestTypes.Arena;
            case 3:
              return QuestTypes.Tutorial;
            case 4:
              return QuestTypes.Free;
            case 5:
              return QuestTypes.StoryExtra;
            case 6:
              return QuestTypes.Event;
            case 7:
              return QuestTypes.Character;
            case 8:
              return QuestTypes.Tower;
            case 9:
              return QuestTypes.VersusFree;
            case 10:
              return QuestTypes.RankMatch;
            case 11:
              return QuestTypes.MultiTower;
            case 12:
              return QuestTypes.MultiGps;
            case 13:
              return QuestTypes.Raid;
            case 14:
              return QuestTypes.GuildRaid;
            case 15:
              return QuestTypes.GenesisStory;
            case 16:
              return QuestTypes.GenesisBoss;
            case 17:
              return QuestTypes.AdvanceStory;
            case 18:
              return QuestTypes.AdvanceBoss;
            case 19:
              return QuestTypes.GvG;
            case 20:
              return QuestTypes.WorldRaid;
          }
        }
      }
      return QuestTypes.Story;
    }

    public bool IsCharacterQuest()
    {
      return this.type == QuestTypes.Character && this.world != null && this.world == "WD_CHARA";
    }

    public static PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
    {
      switch (type)
      {
        case QuestTypes.Multi:
        case QuestTypes.MultiGps:
          return PlayerPartyTypes.Multiplay;
        case QuestTypes.Arena:
          return PlayerPartyTypes.Arena;
        case QuestTypes.Free:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
        case QuestTypes.UnitRental:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        case QuestTypes.StoryExtra:
          return PlayerPartyTypes.StoryExtra;
        case QuestTypes.Ordeal:
          return PlayerPartyTypes.Ordeal;
        case QuestTypes.RankMatch:
          return PlayerPartyTypes.RankMatch;
        case QuestTypes.Raid:
          return PlayerPartyTypes.Raid;
        case QuestTypes.GuildRaid:
          return PlayerPartyTypes.GuildRaid;
        case QuestTypes.GvG:
          return PlayerPartyTypes.GvG;
        case QuestTypes.WorldRaid:
          return PlayerPartyTypes.WorldRaid;
        default:
          return PlayerPartyTypes.Normal;
      }
    }

    public bool IsKeyUnlock(long serverTime = -1)
    {
      return this.Chapter != null && this.Chapter.IsKeyUnlock(serverTime >= 0L ? serverTime : Network.GetServerTime());
    }

    public void GetPartyTypes(
      out PlayerPartyTypes playerPartyType,
      out PlayerPartyTypes enemyPartyType)
    {
      QuestTypes type = this.type;
      switch (type)
      {
        case QuestTypes.Multi:
          playerPartyType = PlayerPartyTypes.Multiplay;
          enemyPartyType = PlayerPartyTypes.Multiplay;
          break;
        case QuestTypes.Arena:
          playerPartyType = PlayerPartyTypes.Arena;
          enemyPartyType = PlayerPartyTypes.ArenaDef;
          break;
        case QuestTypes.Event:
          playerPartyType = PlayerPartyTypes.Event;
          enemyPartyType = PlayerPartyTypes.Event;
          break;
        case QuestTypes.Tower:
          playerPartyType = PlayerPartyTypes.Tower;
          enemyPartyType = PlayerPartyTypes.Tower;
          break;
        case QuestTypes.Gps:
          playerPartyType = PlayerPartyTypes.Event;
          enemyPartyType = PlayerPartyTypes.Event;
          break;
        default:
          if (type != QuestTypes.Beginner)
          {
            if (type != QuestTypes.MultiGps)
            {
              playerPartyType = PlayerPartyTypes.Normal;
              enemyPartyType = PlayerPartyTypes.Normal;
              break;
            }
            goto case QuestTypes.Multi;
          }
          else
            goto case QuestTypes.Event;
      }
    }

    public bool IsAvailable() => MonoSingleton<GameManager>.Instance.Player.IsQuestAvailable(this);

    public static bool TransSectionGotoQuest(
      string questID,
      out QuestTypes quest_type,
      UIUtility.DialogResultEvent callback)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      quest_type = QuestTypes.Story;
      if (string.IsNullOrEmpty(questID))
      {
        QuestParam.TransSectionGotoNormal();
        quest_type = QuestTypes.Story;
        return true;
      }
      QuestParam quest = instance.FindQuest(questID);
      if (quest == null)
      {
        QuestParam.TransSectionGotoNormal();
        quest_type = QuestTypes.Story;
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback);
        return false;
      }
      QuestTypes type = quest.type;
      switch (type)
      {
        case QuestTypes.Event:
        case QuestTypes.Gps:
          quest_type = QuestTypes.Event;
          if (!QuestParam.TransSectionGotoEvent(questID, callback))
            return false;
          break;
        case QuestTypes.Character:
          quest_type = QuestTypes.Character;
          if (!QuestParam.TransSelectionGotoCharacter(questID, callback))
            return false;
          break;
        case QuestTypes.Tower:
          quest_type = QuestTypes.Tower;
          if (!QuestParam.TransSectionGotoTower(questID, out quest_type))
            return false;
          break;
        case QuestTypes.Beginner:
          quest_type = QuestTypes.Beginner;
          if (!QuestParam.TransSectionGotoEvent(questID, callback))
            return false;
          break;
        case QuestTypes.MultiGps:
          if (LevelLock.ShowLockMessage(MonoSingleton<GameManager>.Instance.Player.Lv, MonoSingleton<GameManager>.Instance.Player.VipRank, UnlockTargets.MultiPlay))
            return false;
          quest_type = QuestTypes.Multi;
          break;
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          quest_type = quest.type;
          if (!QuestParam.TransSectionGotoGenesis(questID, callback))
            return false;
          break;
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
          quest_type = quest.type;
          if (!QuestParam.TransSectionGotoAdvance(questID, callback))
            return false;
          break;
        default:
          if (type != QuestTypes.Multi)
          {
            quest_type = QuestTypes.Story;
            if (!QuestParam.TransSectionGotoStory(questID, callback))
              return false;
            break;
          }
          goto case QuestTypes.MultiGps;
      }
      return true;
    }

    public static bool TransSectionGotoNormal()
    {
      FlowNode_SelectLatestChapter.SelectLatestChapter();
      return true;
    }

    public static bool TransSectionGotoElite(UIUtility.DialogResultEvent callback)
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      QuestParam questParam = (QuestParam) null;
      for (int index = availableQuests.Length - 1; index >= 0; --index)
      {
        if ((availableQuests[index].type == QuestTypes.Story || availableQuests[index].type == QuestTypes.Free || availableQuests[index].type == QuestTypes.StoryExtra) && availableQuests[index].difficulty == QuestDifficulties.Elite)
        {
          questParam = availableQuests[index];
          break;
        }
      }
      if (questParam == null)
      {
        QuestParam.TransSectionGotoNormal();
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EQUEST_UNAVAILABLE"), callback);
        return false;
      }
      string chapterId = questParam.ChapterID;
      string str = "WD_01";
      int num = 1;
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == chapterId)
        {
          str = chapters[index].section;
          num = chapters[index].sectionParam.storyPart;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questParam.iname;
      GlobalVars.SelectedChapter.Set(chapterId);
      GlobalVars.SelectedSection.Set(str);
      GlobalVars.SelectedStoryPart.Set(num);
      return true;
    }

    public static bool TransSectionGotoStoryExtra(UIUtility.DialogResultEvent callback)
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      QuestParam questParam = (QuestParam) null;
      for (int index = availableQuests.Length - 1; index >= 0; --index)
      {
        if (availableQuests[index].type == QuestTypes.StoryExtra && availableQuests[index].difficulty == QuestDifficulties.Extra)
        {
          questParam = availableQuests[index];
          break;
        }
      }
      if (questParam == null)
      {
        QuestParam.TransSectionGotoNormal();
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EQUEST_UNAVAILABLE"), callback);
        return false;
      }
      string chapterId = questParam.ChapterID;
      string str = "WD_01";
      int num = 1;
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == chapterId)
        {
          str = chapters[index].section;
          num = chapters[index].sectionParam.storyPart;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questParam.iname;
      GlobalVars.SelectedChapter.Set(chapterId);
      GlobalVars.SelectedSection.Set(str);
      GlobalVars.SelectedStoryPart.Set(num);
      return true;
    }

    public static bool TransSectionGotoStory(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback);
        QuestParam.TransSectionGotoNormal();
        return false;
      }
      string chapterId = quest == null ? (string) null : quest.ChapterID;
      string str = "WD_01";
      int num = 1;
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == chapterId)
        {
          str = chapters[index].section;
          num = chapters[index].sectionParam.storyPart;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questID;
      GlobalVars.SelectedChapter.Set(chapterId);
      GlobalVars.SelectedSection.Set(str);
      GlobalVars.SelectedStoryPart.Set(num);
      return true;
    }

    public static void ClearGenesisIsBossLiberation()
    {
      QuestParam.CachedGenesisIsBossLiberation = (Dictionary<string, bool>) null;
    }

    public static bool TransSectionGotoGenesis(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      GenesisParam genesisParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGenesisParam();
      bool flag = false;
      if (genesisParam != null && genesisParam.IsWithinPeriod() && quest != null)
      {
        if (quest.type == QuestTypes.GenesisBoss)
        {
          if (QuestParam.CachedGenesisIsBossLiberation != null && QuestParam.CachedGenesisIsBossLiberation.ContainsKey(questID))
          {
            flag = QuestParam.CachedGenesisIsBossLiberation[questID];
          }
          else
          {
            if (QuestParam.CachedGenesisIsBossLiberation == null)
              QuestParam.CachedGenesisIsBossLiberation = new Dictionary<string, bool>();
            flag = MonoSingleton<GameManager>.Instance.GetGenesisChapterParamFromAreaId(quest.ChapterID).IsBossLiberation(quest.difficulty);
            QuestParam.CachedGenesisIsBossLiberation.Add(questID, flag);
          }
        }
        else
          flag = player.IsQuestAvailable(questID);
      }
      if (!flag)
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback);
        return false;
      }
      GlobalVars.SelectedQuestID = questID;
      return true;
    }

    public static void ClearAdvanceIsBossLiberation()
    {
      QuestParam.CachedAdvanceIsBossLiberation = (Dictionary<string, bool>) null;
    }

    public static bool TransSectionGotoAdvance(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      bool flag = false;
      if (quest != null)
      {
        if (quest.type == QuestTypes.AdvanceBoss)
        {
          if (QuestParam.CachedAdvanceIsBossLiberation != null && QuestParam.CachedAdvanceIsBossLiberation.ContainsKey(questID))
          {
            flag = QuestParam.CachedAdvanceIsBossLiberation[questID];
          }
          else
          {
            if (QuestParam.CachedAdvanceIsBossLiberation == null)
              QuestParam.CachedAdvanceIsBossLiberation = new Dictionary<string, bool>();
            flag = MonoSingleton<GameManager>.Instance.GetAdvanceEventParamFromAreaId(quest.ChapterID).IsBossLiberation(quest.difficulty);
            QuestParam.CachedAdvanceIsBossLiberation.Add(questID, flag);
          }
        }
        else
          flag = player.IsQuestAvailable(questID);
      }
      if (!flag)
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback);
        return false;
      }
      GlobalVars.SelectedQuestID = questID;
      return true;
    }

    public static bool TransSectionGotoTower(string questID, out QuestTypes quest_type)
    {
      quest_type = QuestTypes.Tower;
      TowerFloorParam towerFloorParam = (TowerFloorParam) null;
      if (!string.IsNullOrEmpty(questID))
      {
        towerFloorParam = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloorParam == null)
        {
          DebugUtility.LogError("[クエストID = " + questID + "]が見つかりません。");
          QuestParam.GotoEventListChapter();
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          quest_type = QuestTypes.Event;
          return true;
        }
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.Tower))
      {
        if (towerFloorParam != null)
        {
          GlobalVars.SelectedTowerID = towerFloorParam.tower_id;
        }
        else
        {
          QuestParam.GotoEventListChapter();
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          quest_type = QuestTypes.Event;
        }
        return true;
      }
      LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Tower);
      return false;
    }

    public static bool TransSectionGotoEvent(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (LevelLock.IsNeedCheckUnlockConds(quest))
      {
        UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
        if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
        {
          QuestParam.GotoEventListChapter();
          return false;
        }
      }
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EVENT_UNAVAILABLE"), callback);
        QuestParam.GotoEventListChapter();
        return false;
      }
      QuestParam.GotoEventListQuest(quest);
      return true;
    }

    public static bool TransSelectionGotoCharacter(
      string questID,
      UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      FlowNode_Variable.Set("CHARA_QUEST_FROM_MISSION", "0");
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (LevelLock.IsNeedCheckUnlockConds(quest))
      {
        UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.CharacterQuest);
        if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
          return false;
      }
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.CHARACTER_UNAVAILABLE"), callback);
        return false;
      }
      FlowNode_Variable.Set("CHARA_QUEST_FROM_MISSION", "1");
      CollaboSkillParam.Pair collaboSkillQuest = QuestParam.GetCollaboSkillQuest(questID);
      if (collaboSkillQuest != null)
      {
        FlowNode_Variable.Set("CHARA_QUEST_TYPE", "COLLABO");
        GlobalVars.SelectedCollaboSkillPair = collaboSkillQuest;
      }
      else
        FlowNode_Variable.Set("CHARA_QUEST_TYPE", "CHARA");
      return true;
    }

    public static void GotoEventListChapter()
    {
      FlowNode_Variable.Set("SHOW_CHAPTER", "1");
      GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
      GlobalVars.SelectedQuestID = (string) null;
      GlobalVars.SelectedChapter.Set((string) null);
      GlobalVars.SelectedSection.Set("WD_DAILY");
      GlobalVars.SelectedStoryPart.Set(1);
    }

    public static void GotoEventListQuest(string questId)
    {
      QuestParam.GotoEventListQuest(string.IsNullOrEmpty(questId) ? (QuestParam) null : MonoSingleton<GameManager>.Instance.FindQuest(questId));
    }

    public static void GotoAdvanceListQuest(ChapterParam chapter)
    {
      if (chapter == null)
        return;
      AdvanceEventParam advanceEventParam = MonoSingleton<GameManager>.Instance.AdvanceEventParamList.Find((Predicate<AdvanceEventParam>) (data => data.ChapterParam == chapter));
      if (advanceEventParam == null)
        return;
      AdvanceManager.CurrentEventParam = advanceEventParam;
      GlobalVars.SelectedQuestID = (string) null;
      GlobalVars.SelectedChapter.Set(chapter.iname);
      GlobalVars.SelectedSection.Set("WD_ADVANCE");
    }

    public static void GotoGenesisListQuest(ChapterParam chapter)
    {
      if (chapter == null)
        return;
      GenesisChapterParam chapterParamFromAreaId = MonoSingleton<GameManager>.Instance.GetGenesisChapterParamFromAreaId(chapter.iname);
      if (chapterParamFromAreaId == null)
        return;
      GenesisManager.CurrentChapterParam = chapterParamFromAreaId;
      GlobalVars.SelectedQuestID = (string) null;
      GlobalVars.SelectedChapter.Set(chapter.iname);
      GlobalVars.SelectedSection.Set("WD_GENESIS");
    }

    public static void GotoEventListQuest(QuestParam quest)
    {
      string iname = quest == null ? (string) null : quest.iname;
      string chapterId = quest == null ? (string) null : quest.ChapterID;
      ChapterParam area = chapterId == null ? (ChapterParam) null : MonoSingleton<GameManager>.Instance.FindArea(chapterId);
      if (area != null && area.IsKeyQuest())
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", "1");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
        GlobalVars.SelectedQuestID = (string) null;
        GlobalVars.SelectedChapter.Set((string) null);
        GlobalVars.SelectedSection.Set("WD_DAILY");
        GlobalVars.SelectedStoryPart.Set(1);
      }
      else
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", string.IsNullOrEmpty(iname) || string.IsNullOrEmpty(chapterId) ? "1" : "0");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
        GlobalVars.SelectedQuestID = iname;
        GlobalVars.SelectedChapter.Set(chapterId);
        if (area != null && area.IsSeiseki())
        {
          GlobalVars.SelectedSection.Set("WD_SEISEKI");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Seiseki;
        }
        else if (area != null && area.IsBabel())
        {
          GlobalVars.SelectedSection.Set("WD_BABEL");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Babel;
        }
        else
        {
          GlobalVars.SelectedSection.Set("WD_DAILY");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
        }
        GlobalVars.SelectedStoryPart.Set(1);
        if (quest == null || quest.type != QuestTypes.Event || quest.subtype != SubQuestTypes.Normal)
          return;
        GlobalVars.SelectedSection.Set("WD_DAILY");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.DailyAndEnhance;
      }
    }

    public static void GotoEventListQuest(ChapterParam chapter)
    {
      if (chapter != null && chapter.IsKeyQuest())
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", "0");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
        GlobalVars.SelectedQuestID = (string) null;
        GlobalVars.SelectedChapter.Set((string) null);
        GlobalVars.SelectedSection.Set("WD_DAILY");
        GlobalVars.SelectedStoryPart.Set(1);
      }
      else
      {
        FlowNode_Variable.Set("SHOW_CHAPTER", "0");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
        GlobalVars.SelectedQuestID = (string) null;
        GlobalVars.SelectedChapter.Set(chapter.iname);
        if (chapter != null && chapter.IsSeiseki())
        {
          GlobalVars.SelectedSection.Set("WD_SEISEKI");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Seiseki;
        }
        else if (chapter != null && chapter.IsBabel())
        {
          GlobalVars.SelectedSection.Set("WD_BABEL");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Babel;
        }
        else
        {
          GlobalVars.SelectedSection.Set("WD_DAILY");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
        }
        GlobalVars.SelectedStoryPart.Set(1);
        QuestParam questParam = chapter.quests.FirstOrDefault<QuestParam>();
        if (questParam == null || questParam.subtype != SubQuestTypes.Normal)
          return;
        GlobalVars.SelectedSection.Set("WD_DAILY");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.DailyAndEnhance;
      }
    }

    public PartyCondType GetPartyCondType()
    {
      if (this.type == QuestTypes.Character)
      {
        if (this.EntryConditionCh != null)
          return this.EntryConditionCh.party_type;
      }
      else if (this.EntryCondition != null)
        return this.EntryCondition.party_type;
      return this.questParty != null && !((IEnumerable<PartySlotTypeUnitPair>) this.questParty.GetMainSubSlots()).Any<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.Free)) ? PartyCondType.Forced : PartyCondType.None;
    }

    public QuestCondParam GetQuestCondParam()
    {
      return this.type == QuestTypes.Character ? this.EntryConditionCh : this.EntryCondition;
    }

    public bool IsOpenUnitHave()
    {
      return string.IsNullOrEmpty(this.OpenUnit) || MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.OpenUnit) != null;
    }

    public static CollaboSkillParam.Pair GetCollaboSkillQuest(string quest_id)
    {
      CollaboSkillParam.Pair collaboSkillQuest = (CollaboSkillParam.Pair) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return (CollaboSkillParam.Pair) null;
      QuestParam questParam1 = Array.Find<QuestParam>(instance.Quests, (Predicate<QuestParam>) (p => p.iname == quest_id));
      if (questParam1 == null)
        return (CollaboSkillParam.Pair) null;
      List<QuestParam> chapterIdQuestParam = QuestParam.GetSameChapterIDQuestParam(questParam1.ChapterID);
      if (chapterIdQuestParam == null)
        return (CollaboSkillParam.Pair) null;
      foreach (QuestParam questParam2 in chapterIdQuestParam)
      {
        collaboSkillQuest = CollaboSkillParam.IsLearnQuest(questParam2.iname);
        if (collaboSkillQuest != null)
          break;
      }
      return collaboSkillQuest;
    }

    public static List<QuestParam> GetSameChapterIDQuestParam(string chapter_id)
    {
      List<QuestParam> chapterIdQuestParam = (List<QuestParam>) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return chapterIdQuestParam;
      QuestParam[] all = Array.FindAll<QuestParam>(instance.Quests, (Predicate<QuestParam>) (p => p.ChapterID == chapter_id));
      return all == null ? chapterIdQuestParam : new List<QuestParam>((IEnumerable<QuestParam>) all);
    }

    public static int GetRaidTicketCount_LimitMax(QuestParam quest, int ini_max)
    {
      if (quest == null)
        return ini_max;
      int ticketCountLimitMax = ini_max;
      if (quest.type == QuestTypes.StoryExtra && MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax > 0)
      {
        int restChallengeCount = MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.RestChallengeCount;
        ticketCountLimitMax = Mathf.Min(ticketCountLimitMax, restChallengeCount);
      }
      if (quest.GetChapterChallangeLimit() > 0)
      {
        int num = quest.GetChapterChallangeLimit() - quest.GetChapterChallangeCount();
        ticketCountLimitMax = Mathf.Min(ticketCountLimitMax, num);
      }
      if (quest.GetChallangeLimit() > 0)
      {
        int num = quest.GetChallangeLimit() - quest.GetChallangeCount();
        ticketCountLimitMax = Mathf.Min(ticketCountLimitMax, num);
      }
      return ticketCountLimitMax;
    }

    public bool HasMission() => this.bonusObjective != null && this.bonusObjective.Length > 0;

    public bool IsMissionClear(int index) => (this.clear_missions & 1 << index) != 0;

    public bool IsMissionCompleteALL()
    {
      if (this.bonusObjective == null || this.bonusObjective.Length <= 0)
        return false;
      for (int index = 0; index < this.bonusObjective.Length; ++index)
      {
        if (!this.IsMissionClear(index))
          return false;
      }
      return true;
    }

    public int GetClearMissionNum()
    {
      int clearMissionNum = 0;
      if (this.bonusObjective != null && this.bonusObjective.Length > 0)
      {
        for (int index = 0; index < this.bonusObjective.Length; ++index)
        {
          if (this.IsMissionClear(index))
            ++clearMissionNum;
        }
      }
      return clearMissionNum;
    }

    public void SetMissionFlag(int index, bool isClear)
    {
      if (this.bonusObjective == null || index >= this.bonusObjective.Length)
        return;
      if (isClear)
        this.clear_missions |= 1 << index;
      else
        this.clear_missions &= ~(1 << index);
    }

    public void SetMissionValue(int index, int value)
    {
      if (this.mission_values == null || index >= this.mission_values.Length)
        return;
      this.mission_values[index] = value;
    }

    public int GetMissionValue(int index)
    {
      return this.mission_values == null || index >= this.mission_values.Length ? 0 : this.mission_values[index];
    }

    public bool CheckMissionValueIsDefault(int index) => this.GetMissionValue(index) == -1;

    public static QuestDifficulties GetQuestDifficulties(string value)
    {
      if (string.IsNullOrEmpty(value))
        return QuestDifficulties.Normal;
      switch (value)
      {
        case "NORMAL":
          return QuestDifficulties.Normal;
        case "HARD":
          return QuestDifficulties.Elite;
        case "EXTRA":
          return QuestDifficulties.Extra;
        default:
          return QuestDifficulties.Normal;
      }
    }

    public string GetClearBestTime()
    {
      string clearBestTime = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CLEAR_BEST_TIME_NONE");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && this.best_clear_time > 0)
      {
        float questPlayTime = MonoSingleton<GameManager>.Instance.GetQuestPlayTime();
        int num1 = (double) questPlayTime <= 0.0 || (int) questPlayTime >= this.best_clear_time ? this.best_clear_time : (int) questPlayTime;
        int num2 = num1 / 60;
        clearBestTime = (num2 / 60).ToString("00") + ":" + (num2 % 60).ToString("00") + ":" + (num1 % 60).ToString("00");
      }
      return clearBestTime;
    }

    private enum BitType
    {
      ShowReviewPopup,
      notSearch,
      IsBeginner,
      hidden,
      replayLimit,
      AllowRetreat,
      AllowAutoPlay,
      Silent,
      DisableAbilities,
      DisableItems,
      DisableContinue,
      UseFixEditor,
      IsNoStartVoice,
      UseSupportUnit,
      UnitChange,
      IsMultiLeaderSkill,
      FirstAutoPlayProbihit,
      IsWeatherNoChange,
      IsAutoRepeatQuest,
      MAX_BIT_ARRAY,
    }

    [Flags]
    public enum Tags : byte
    {
      MAL = 1,
      FEM = 2,
      HERO = 4,
    }
  }
}
