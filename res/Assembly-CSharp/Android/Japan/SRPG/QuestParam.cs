﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class QuestParam
  {
    private static readonly int MULTI_MAX_TOTAL_UNIT = 4;
    private static readonly int MULTI_MAX_PLAYER_UNIT = 2;
    private BitArray bit_array = new BitArray(18);
    private short cond_index = -1;
    private short world_index = -1;
    private short ChapterID_index = -1;
    public ShareStringList units = new ShareStringList(ShareString.Type.QuestParam_units);
    public OShort clock_win = (OShort) 0;
    public OShort clock_lose = (OShort) 0;
    public OShort win = (OShort) 0;
    public OShort lose = (OShort) 0;
    public OShort multi = (OShort) 0;
    public OShort multiDead = (OShort) 0;
    public OShort playerNum = (OShort) 0;
    public OShort unitNum = (OShort) 0;
    public List<MapParam> map = new List<MapParam>(BattleCore.MAX_MAP);
    private short ticket_index = -1;
    public string iname;
    public string title;
    public string name;
    public string expr;
    public string mission;
    public string[] cond_quests;
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
    public QuestTypes type;
    public SubQuestTypes subtype;
    public short lv;
    public string event_start;
    public string event_clear;
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

    public bool notSearch
    {
      set
      {
        this.bit_array.Set(1, value);
      }
      get
      {
        return this.bit_array.Get(1);
      }
    }

    public int dayReset { get; set; }

    public bool IsMulti
    {
      get
      {
        return (int) this.multi != 0;
      }
    }

    public bool IsMultiEvent
    {
      get
      {
        return (int) this.multi >= 100;
      }
    }

    public bool IsMultiVersus
    {
      get
      {
        if ((int) this.multi != 2)
          return (int) this.multi == 102;
        return true;
      }
    }

    public bool IsMultiAreaQuest
    {
      get
      {
        return this.type == QuestTypes.MultiGps;
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
      set
      {
        this.bit_array.Set(5, value);
      }
      get
      {
        return this.bit_array.Get(5);
      }
    }

    public bool AllowAutoPlay
    {
      set
      {
        this.bit_array.Set(6, value);
      }
      get
      {
        return this.bit_array.Get(6);
      }
    }

    public bool FirstAutoPlayProhibit
    {
      set
      {
        this.bit_array.Set(16, value);
      }
      get
      {
        return this.bit_array.Get(16);
      }
    }

    public bool Silent
    {
      set
      {
        this.bit_array.Set(7, value);
      }
      get
      {
        return this.bit_array.Get(7);
      }
    }

    public bool DisableAbilities
    {
      set
      {
        this.bit_array.Set(8, value);
      }
      get
      {
        return this.bit_array.Get(8);
      }
    }

    public bool DisableItems
    {
      set
      {
        this.bit_array.Set(9, value);
      }
      get
      {
        return this.bit_array.Get(9);
      }
    }

    public bool DisableContinue
    {
      set
      {
        this.bit_array.Set(10, value);
      }
      get
      {
        return this.bit_array.Get(10);
      }
    }

    public bool IsUnitChange
    {
      set
      {
        this.bit_array.Set(14, value);
      }
      get
      {
        return this.bit_array.Get(14);
      }
    }

    public bool IsMultiLeaderSkill
    {
      set
      {
        this.bit_array.Set(15, value);
      }
      get
      {
        return this.bit_array.Get(15);
      }
    }

    public bool IsWeatherNoChange
    {
      set
      {
        this.bit_array.Set(17, value);
      }
      get
      {
        return this.bit_array.Get(17);
      }
    }

    public bool hidden
    {
      set
      {
        this.bit_array.Set(3, value);
      }
      get
      {
        return this.bit_array.Get(3);
      }
    }

    public bool replayLimit
    {
      set
      {
        this.bit_array.Set(4, value);
      }
      get
      {
        return this.bit_array.Get(4);
      }
    }

    public bool ShowReviewPopup
    {
      set
      {
        this.bit_array.Set(0, value);
      }
      get
      {
        return this.bit_array.Get(0);
      }
    }

    public bool IsScenario
    {
      get
      {
        if (this.map.Count != 0)
          return string.IsNullOrEmpty(this.map[0].mapSetName);
        return true;
      }
    }

    public bool IsStory
    {
      get
      {
        return this.type == QuestTypes.Story;
      }
    }

    public bool IsGps
    {
      get
      {
        return this.type == QuestTypes.Gps;
      }
    }

    public bool IsVersus
    {
      get
      {
        if (this.type != QuestTypes.VersusFree)
          return this.type == QuestTypes.VersusRank;
        return true;
      }
    }

    public bool IsRankMatch
    {
      get
      {
        return this.type == QuestTypes.RankMatch;
      }
    }

    public bool IsKeyQuest
    {
      get
      {
        if (this.Chapter != null)
          return this.Chapter.IsKeyQuest();
        return false;
      }
    }

    public bool IsQuestDrops
    {
      get
      {
        if (this.type != QuestTypes.Story && this.type != QuestTypes.Free && (this.type != QuestTypes.Extra && this.type != QuestTypes.Character) && (this.type != QuestTypes.Event && this.type != QuestTypes.Multi && (this.type != QuestTypes.Gps && this.type != QuestTypes.Beginner)))
          return this.type == QuestTypes.MultiGps;
        return true;
      }
    }

    public bool IsMultiTower
    {
      get
      {
        return this.type == QuestTypes.MultiTower;
      }
    }

    public bool IsRaid
    {
      get
      {
        return this.type == QuestTypes.Raid;
      }
    }

    public bool IsGenesisStory
    {
      get
      {
        return this.type == QuestTypes.GenesisStory;
      }
    }

    public bool IsGenesisBoss
    {
      get
      {
        return this.type == QuestTypes.GenesisBoss;
      }
    }

    public bool IsGenesis
    {
      get
      {
        if (!this.IsGenesisStory)
          return this.IsGenesisBoss;
        return true;
      }
    }

    public int GainPlayerExp
    {
      get
      {
        return this.pexp;
      }
    }

    public int GainUnitExp
    {
      get
      {
        return this.uexp;
      }
    }

    public int OverClockTimeWin
    {
      get
      {
        return (int) this.clock_win;
      }
    }

    public int OverClockTimeLose
    {
      get
      {
        return (int) this.clock_lose;
      }
    }

    public bool IsBeginner
    {
      set
      {
        this.bit_array.Set(2, value);
      }
      get
      {
        return this.bit_array.Get(2);
      }
    }

    public bool UseFixEditor
    {
      set
      {
        this.bit_array.Set(11, value);
      }
      get
      {
        return this.bit_array.Get(11);
      }
    }

    public bool IsNoStartVoice
    {
      set
      {
        this.bit_array.Set(12, value);
      }
      get
      {
        return this.bit_array.Get(12);
      }
    }

    public bool UseSupportUnit
    {
      set
      {
        this.bit_array.Set(13, value);
      }
      get
      {
        return this.bit_array.Get(13);
      }
    }

    public int MissionNum
    {
      get
      {
        if (!this.HasMission())
          return 0;
        return this.bonusObjective.Length;
      }
    }

    public int GetAtkTypeMag(AttackDetailTypes type)
    {
      if (this.AtkTypeMags != null)
        return this.AtkTypeMags[(int) type];
      return 0;
    }

    public void SetAtkTypeMag(int[] mags)
    {
      this.AtkTypeMags = mags;
    }

    public void Deserialize(JSON_QuestParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
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
      if (this.EntryCondition == null)
        return true;
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
      if (condition == null)
      {
        if (this.type == QuestTypes.Tower)
          DebugUtility.LogError("塔 コンディションが入っていません iname = " + this.iname);
        return true;
      }
      if (unit == null)
        return false;
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
      int num3 = unit.CalcLevel();
      if (condition.ulvmax > 0 && num3 > condition.ulvmax)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (condition.ulvmin > 0 && num3 < condition.ulvmin)
      {
        error = "sys.PARTYEDITOR_ULV";
        return false;
      }
      if (this.type == QuestTypes.Tower && MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UniqueID) != null)
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        if (towerResuponse.pdeck != null)
        {
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => unit.UnitParam.iname == x.unitname));
          if (playerUnit != null && playerUnit.isDied)
          {
            error = "sys.ERROR_TOWER_DEAD_UNIT";
            return false;
          }
        }
      }
      return true;
    }

    public List<string> GetEntryQuestConditions(bool titled = true, bool includeUnitLv = true, bool includeUnits = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryCondition, titled, includeUnitLv, includeUnits);
    }

    public List<string> GetEntryQuestConditionsCh(bool titled = true, bool includeUnitLv = true, bool includeUnits = true)
    {
      return this.GetEntryQuestConditionsInternal(this.EntryConditionCh, titled, includeUnitLv, includeUnits);
    }

    private List<string> GetEntryQuestConditionsInternal(QuestCondParam condParam, bool titled = true, bool includeUnitLv = true, bool includeUnits = true)
    {
      List<string> stringList1 = new List<string>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
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
        stringList1.Add(str2);
      }
      if (condParam != null && includeUnitLv && (condParam.ulvmin > 0 || condParam.ulvmax > 0))
      {
        int unitMaxLevel = instanceDirect.MasterParam.GetUnitMaxLevel();
        int num1 = Math.Max(condParam.ulvmin, 1);
        int num2 = Math.Min(condParam.ulvmax, unitMaxLevel);
        string str1 = string.Empty;
        string str2;
        if (titled)
        {
          string str3 = LocalizedText.Get("sys.PARTYEDITOR_COND_ULV");
          if (num1 > 0)
            str3 += (string) (object) num1;
          str2 = str3 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 += (string) (object) num2;
        }
        else
        {
          if (num1 > 0)
            str1 = str1 + LocalizedText.Get("sys.ULV") + (object) num1;
          str2 = str1 + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str2 = str2 + LocalizedText.Get("sys.ULV") + (object) num2;
        }
        stringList1.Add(str2);
      }
      if (includeUnits)
      {
        string[] strArray = condParam?.unit;
        if (condParam != null && condParam.party_type == PartyCondType.Limited)
          strArray = condParam.unit;
        else if (this.questParty != null)
          strArray = !((IEnumerable<PartySlotTypeUnitPair>) this.questParty.GetMainSubSlots()).Any<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.Free)) ? ((IEnumerable<PartySlotTypeUnitPair>) this.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot =>
          {
            if (slot.Type != PartySlotType.Forced && slot.Type != PartySlotType.ForcedHero && slot.Type != PartySlotType.Npc)
              return slot.Type == PartySlotType.NpcHero;
            return true;
          })).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>() : (string[]) null;
        else if (condParam != null && condParam.party_type == PartyCondType.Forced)
          strArray = condParam.unit;
        if (strArray != null && strArray.Length > 0)
        {
          List<string> stringList2 = new List<string>();
          for (int index = 0; index < strArray.Length; ++index)
          {
            UnitParam unitParam = instanceDirect.GetUnitParam(strArray[index]);
            if (unitParam != null && !stringList2.Contains(unitParam.name))
              stringList2.Add(unitParam.name);
          }
          if (stringList2.Count > 0)
          {
            string str1 = string.Empty;
            for (int index = 0; index < stringList2.Count; ++index)
              str1 = str1 + (index <= 0 ? string.Empty : "、") + stringList2[index];
            if (!string.IsNullOrEmpty(str1))
            {
              string empty = string.Empty;
              if (titled)
                empty = LocalizedText.Get("sys.PARTYEDITOR_COND_UNIT");
              string str2 = empty + str1;
              stringList1.Add(str2);
            }
          }
        }
      }
      if (condParam != null && condParam.job != null && condParam.job.Length > 0)
      {
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < condParam.job.Length; ++index)
        {
          JobParam jobParam = instanceDirect.GetJobParam(condParam.job[index]);
          if (jobParam != null && !stringList2.Contains(jobParam.name))
            stringList2.Add(jobParam.name);
        }
        if (stringList2.Count > 0)
        {
          string str1 = string.Empty;
          for (int index = 0; index < stringList2.Count; ++index)
            str1 = str1 + (index <= 0 ? string.Empty : "、") + stringList2[index];
          if (!string.IsNullOrEmpty(str1))
          {
            string empty = string.Empty;
            if (titled)
              empty = LocalizedText.Get("sys.PARTYEDITOR_COND_JOB");
            string str2 = empty + str1;
            stringList1.Add(str2);
          }
        }
      }
      if (condParam != null && condParam.jobset != null && (condParam.jobset.Length > 0 && Array.IndexOf<int>(condParam.jobset, 1) != -1))
      {
        string str1 = string.Empty;
        int index = 0;
        int num1 = 0;
        for (; index < condParam.jobset.Length; ++index)
        {
          if (condParam.jobset[index] != 0)
          {
            int num2 = index + 1;
            str1 = str1 + (num1 <= 0 ? (object) string.Empty : (object) "、") + (object) num2;
            ++num1;
          }
        }
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX");
          string str2 = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX_VALUE", new object[1]{ (object) str1 });
          stringList1.Add(str2);
        }
      }
      if (condParam != null && (condParam.rmin > 0 || condParam.rmax > 0))
      {
        string empty = string.Empty;
        if (titled)
          empty = LocalizedText.Get("sys.PARTYEDITOR_COND_RARITY");
        int num1 = Math.Max(condParam.rmin - 1, 0);
        int num2 = Math.Max(condParam.rmax - 1, 0);
        string str;
        if (num1 == num2)
        {
          str = empty + LocalizedText.Get("sys.RARITY_STAR_" + (object) num1);
        }
        else
        {
          if (num1 > 0)
            empty += LocalizedText.Get("sys.RARITY_STAR_" + (object) num1);
          str = empty + LocalizedText.Get("sys.TILDE");
          if (num2 > 0)
            str += LocalizedText.Get("sys.RARITY_STAR_" + (object) num2);
        }
        stringList1.Add(str);
      }
      if (condParam != null && condParam.isElemLimit)
      {
        string str1 = string.Empty;
        int index = 0;
        int num = 0;
        for (; index < condParam.elem.Length; ++index)
        {
          if (index != 0 && condParam.elem[index] != 0)
          {
            str1 = str1 + (num <= 0 ? string.Empty : "、") + LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) index);
            ++num;
          }
        }
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_ELEM");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam != null && condParam.birth != null && condParam.birth.Length > 0)
      {
        string str1 = string.Empty;
        for (int index = 0; index < condParam.birth.Length; ++index)
          str1 = str1 + (index <= 0 ? string.Empty : "、") + condParam.birth[index];
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_BIRTH");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam != null && condParam.sex != ESex.Unknown)
      {
        string str1 = LocalizedText.Get("sys.SEX_" + (object) condParam.sex);
        if (!string.IsNullOrEmpty(str1))
        {
          string empty = string.Empty;
          if (titled)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_SEX");
          string str2 = empty + str1;
          stringList1.Add(str2);
        }
      }
      if (condParam != null && (condParam.hmin > 0 || condParam.hmax > 0))
      {
        int hmin = condParam.hmin;
        int hmax = condParam.hmax;
        string str1 = string.Empty;
        if (titled)
          str1 = LocalizedText.Get("sys.PARTYEDITOR_COND_HEIGHT");
        if (hmin > 0)
          str1 = str1 + (object) hmin + LocalizedText.Get("sys.CM_HEIGHT");
        string str2 = str1 + LocalizedText.Get("sys.TILDE");
        if (hmax > 0)
          str2 = str2 + (object) hmax + LocalizedText.Get("sys.CM_HEIGHT");
        stringList1.Add(str2);
      }
      if (condParam != null && (condParam.wmin > 0 || condParam.wmax > 0))
      {
        int wmin = condParam.wmin;
        int wmax = condParam.wmax;
        string str1 = string.Empty;
        if (titled)
          str1 = LocalizedText.Get("sys.PARTYEDITOR_COND_WEIGHT");
        if (wmin > 0)
          str1 = str1 + (object) wmin + LocalizedText.Get("sys.KG_WEIGHT");
        string str2 = str1 + LocalizedText.Get("sys.TILDE");
        if (wmax > 0)
          str2 = str2 + (object) wmax + LocalizedText.Get("sys.KG_WEIGHT");
        stringList1.Add(str2);
      }
      return stringList1;
    }

    public List<string> GetAddQuestInfo(bool is_inc_title = true)
    {
      List<string> stringList = new List<string>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return stringList;
      if (!string.IsNullOrEmpty(this.MapEffectId))
      {
        MapEffectParam mapEffectParam = instanceDirect.GetMapEffectParam(this.MapEffectId);
        if (mapEffectParam != null)
        {
          string empty = string.Empty;
          if (is_inc_title)
            empty = LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT");
          string str = empty + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_HEAD") + mapEffectParam.Name + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_BOTTOM");
          stringList.Add(str);
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
            stringList.Add(str2);
          }
        }
      }
      return stringList;
    }

    private void AddWeatherNameLists(List<string> wth_name_lists, List<string> wth_id_lists)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null || wth_id_lists == null)
        return;
      foreach (string wthIdList in wth_id_lists)
      {
        WeatherParam weatherParam = instanceDirect.MasterParam.GetWeatherParam(wthIdList);
        if (weatherParam != null && !wth_name_lists.Contains(weatherParam.Name))
          wth_name_lists.Add(weatherParam.Name);
      }
    }

    public bool IsJigen
    {
      get
      {
        return this.end != 0L;
      }
    }

    public bool IsDateUnlock(long serverTime = -1)
    {
      long num = serverTime >= 0L ? serverTime : Network.GetServerTime();
      if (!MonoSingleton<GameManager>.Instance.Player.IsBeginner() && this.IsBeginner)
        return false;
      if (!this.IsJigen)
        return !this.hidden;
      return this.start <= num && num < this.end;
    }

    public bool IsReplayDateUnlock(long serverTime = -1)
    {
      if (!this.replayLimit)
        return true;
      return this.IsDateUnlock(serverTime);
    }

    public int GetSelectMainMemberNum()
    {
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      int num = 0;
      switch (this.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Tutorial:
        case QuestTypes.Character:
        case QuestTypes.MultiGps:
          num = party.MAX_MAINMEMBER - 1;
          break;
        case QuestTypes.Arena:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.Extra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          num = party.MAX_MAINMEMBER;
          break;
      }
      return num;
    }

    public bool CheckEnableEntrySubMembers()
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
        case QuestTypes.Extra:
        case QuestTypes.Beginner:
        case QuestTypes.Raid:
          return true;
        default:
          return false;
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
        case QuestTypes.Extra:
        case QuestTypes.Beginner:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          if (this.FirstAutoPlayProhibit && this.state != QuestStates.Cleared)
            return false;
          return this.AllowAutoPlay;
        default:
          return false;
      }
    }

    public bool CheckAllowedRetreat()
    {
      if (this.type == QuestTypes.Tutorial)
        return false;
      return this.AllowRetreat;
    }

    public bool CheckDisableAbilities()
    {
      return this.DisableAbilities;
    }

    public bool CheckDisableItems()
    {
      return this.DisableItems;
    }

    public bool CheckDisableContinue()
    {
      return this.DisableContinue;
    }

    public bool CheckEnableQuestResult()
    {
      return this.type != QuestTypes.Tutorial && this.type != QuestTypes.Arena;
    }

    public bool CheckEnableGainedItem()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableGainedGold()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableGainedExp()
    {
      return this.type != QuestTypes.Tutorial;
    }

    public bool CheckEnableSuspendStart()
    {
      return this.type != QuestTypes.Tutorial && this.type != QuestTypes.Arena && (this.type != QuestTypes.Multi && this.type != QuestTypes.MultiGps);
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
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        return this.key_cnt;
      return (int) this.dailyCount;
    }

    public int GetChallangeLimit()
    {
      if (this.IsKeyQuest && this.Chapter != null && this.Chapter.GetKeyQuestType() == KeyQuestTypes.Count)
        return this.key_limit;
      return (int) this.challengeLimit;
    }

    public bool CheckEnableChallange()
    {
      int challangeLimit = this.GetChallangeLimit();
      return challangeLimit == 0 || this.GetChallangeCount() < challangeLimit;
    }

    public bool CheckEnableReset()
    {
      if (this.difficulty != QuestDifficulties.Elite && this.difficulty != QuestDifficulties.Extra)
        return false;
      return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.EliteResetMax > (int) this.dailyReset;
    }

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
          QuestParam.\u003C\u003Ef__switch\u0024map3 = new Dictionary<string, int>(16)
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
              "Event",
              5
            },
            {
              "Character",
              6
            },
            {
              "tower",
              7
            },
            {
              "vs",
              8
            },
            {
              "vs_tower",
              8
            },
            {
              "rm",
              9
            },
            {
              "multi_tower",
              10
            },
            {
              "multi_areaquest",
              11
            },
            {
              "raidboss",
              12
            },
            {
              "genesis",
              13
            },
            {
              "genesis_raid",
              14
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
              return QuestTypes.Event;
            case 6:
              return QuestTypes.Character;
            case 7:
              return QuestTypes.Tower;
            case 8:
              return QuestTypes.VersusFree;
            case 9:
              return QuestTypes.RankMatch;
            case 10:
              return QuestTypes.MultiTower;
            case 11:
              return QuestTypes.MultiGps;
            case 12:
              return QuestTypes.Raid;
            case 13:
              return QuestTypes.GenesisStory;
            case 14:
              return QuestTypes.GenesisBoss;
          }
        }
      }
      return QuestTypes.Story;
    }

    public bool IsCharacterQuest()
    {
      if (this.type == QuestTypes.Character && this.world != null)
        return this.world == "WD_CHARA";
      return false;
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
        case QuestTypes.Extra:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        case QuestTypes.RankMatch:
          return PlayerPartyTypes.RankMatch;
        case QuestTypes.Raid:
          return PlayerPartyTypes.Raid;
        default:
          return PlayerPartyTypes.Normal;
      }
    }

    public bool IsKeyUnlock(long serverTime = -1)
    {
      if (this.Chapter != null)
        return this.Chapter.IsKeyUnlock(serverTime >= 0L ? serverTime : Network.GetServerTime());
      return false;
    }

    public void GetPartyTypes(out PlayerPartyTypes playerPartyType, out PlayerPartyTypes enemyPartyType)
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

    public static bool TransSectionGotoQuest(string questID, out QuestTypes quest_type, UIUtility.DialogResultEvent callback)
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
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
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
          quest_type = QuestTypes.Multi;
          break;
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          quest_type = quest.type;
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
        if (availableQuests[index].difficulty == QuestDifficulties.Elite)
        {
          questParam = availableQuests[index];
          break;
        }
      }
      if (questParam == null)
      {
        QuestParam.TransSectionGotoNormal();
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EQUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
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
        if (availableQuests[index].difficulty == QuestDifficulties.Extra)
        {
          questParam = availableQuests[index];
          break;
        }
      }
      if (questParam == null)
      {
        QuestParam.TransSectionGotoNormal();
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EQUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
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
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        QuestParam.TransSectionGotoNormal();
        return false;
      }
      string str1 = quest == null ? (string) null : quest.ChapterID;
      string str2 = "WD_01";
      int num = 1;
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == str1)
        {
          str2 = chapters[index].section;
          num = chapters[index].sectionParam.storyPart;
          break;
        }
      }
      GlobalVars.SelectedQuestID = questID;
      GlobalVars.SelectedChapter.Set(str1);
      GlobalVars.SelectedSection.Set(str2);
      GlobalVars.SelectedStoryPart.Set(num);
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
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.EVENT_UNAVAILABLE"), callback, (GameObject) null, false, -1);
        QuestParam.GotoEventListChapter();
        return false;
      }
      QuestParam.GotoEventListQuest(quest);
      return true;
    }

    public static bool TransSelectionGotoCharacter(string questID, UIUtility.DialogResultEvent callback)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      FlowNode_Variable.Set("CHARA_QUEST_FROM_MISSION", "0");
      if (!player.IsQuestAvailable(questID))
      {
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.CHARACTER_UNAVAILABLE"), callback, (GameObject) null, false, -1);
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

    public static void GotoEventListQuest(QuestParam quest)
    {
      string str = quest == null ? (string) null : quest.iname;
      string iname = quest == null ? (string) null : quest.ChapterID;
      ChapterParam chapterParam = iname == null ? (ChapterParam) null : MonoSingleton<GameManager>.Instance.FindArea(iname);
      if (chapterParam != null && chapterParam.IsKeyQuest())
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
        FlowNode_Variable.Set("SHOW_CHAPTER", string.IsNullOrEmpty(str) || string.IsNullOrEmpty(iname) ? "1" : "0");
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
        GlobalVars.SelectedQuestID = str;
        GlobalVars.SelectedChapter.Set(iname);
        if (chapterParam != null && chapterParam.IsSeiseki())
        {
          GlobalVars.SelectedSection.Set("WD_SEISEKI");
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Seiseki;
        }
        else if (chapterParam != null && chapterParam.IsBabel())
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
      if (this.type == QuestTypes.Character)
        return this.EntryConditionCh;
      return this.EntryCondition;
    }

    public static CollaboSkillParam.Pair GetCollaboSkillQuest(string quest_id)
    {
      CollaboSkillParam.Pair pair = (CollaboSkillParam.Pair) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return (CollaboSkillParam.Pair) null;
      QuestParam questParam1 = Array.Find<QuestParam>(instance.Quests, (Predicate<QuestParam>) (p => p.iname == quest_id));
      if (questParam1 == null)
        return (CollaboSkillParam.Pair) null;
      List<QuestParam> chapterIdQuestParam = QuestParam.GetSameChapterIDQuestParam(questParam1.ChapterID);
      if (chapterIdQuestParam == null)
        return (CollaboSkillParam.Pair) null;
      foreach (QuestParam questParam2 in chapterIdQuestParam)
      {
        pair = CollaboSkillParam.IsLearnQuest(questParam2.iname);
        if (pair != null)
          break;
      }
      return pair;
    }

    public static List<QuestParam> GetSameChapterIDQuestParam(string chapter_id)
    {
      List<QuestParam> questParamList = (List<QuestParam>) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return questParamList;
      QuestParam[] all = Array.FindAll<QuestParam>(instance.Quests, (Predicate<QuestParam>) (p => p.ChapterID == chapter_id));
      if (all == null)
        return questParamList;
      return new List<QuestParam>((IEnumerable<QuestParam>) all);
    }

    public bool HasMission()
    {
      return this.bonusObjective != null && this.bonusObjective.Length > 0;
    }

    public bool IsMissionClear(int index)
    {
      return (this.clear_missions & 1 << index) != 0;
    }

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
      int num = 0;
      if (this.bonusObjective != null && this.bonusObjective.Length > 0)
      {
        for (int index = 0; index < this.bonusObjective.Length; ++index)
        {
          if (this.IsMissionClear(index))
            ++num;
        }
      }
      return num;
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
      if (this.mission_values == null || index >= this.mission_values.Length)
        return 0;
      return this.mission_values[index];
    }

    public bool CheckMissionValueIsDefault(int index)
    {
      return this.GetMissionValue(index) == -1;
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
