﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerFloorParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class MultiTowerFloorParam
  {
    public List<MapParam> map = new List<MapParam>(BattleCore.MAX_MAP);
    private QuestParam CachedQuestParam;
    private QuestParam BaseQuest;
    public int id;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string tower_id;
    public int cond_floor;
    public string reward_id;
    public short pt;
    public short FloorIndex;
    public short floor;
    public short lv;
    public short joblv;
    public short unitnum;
    public short notcon;
    public bool DownLoaded;
    public string error_messarge;
    public string iname_text;
    public string me_id;
    public int is_wth_no_chg;
    public string wth_set_id;
    public bool is_skip;
    public string entry_condition;

    public QuestParam Clone(QuestParam original, bool useCache = false)
    {
      if (!useCache)
        return this.ConvertQuestParam(original == null ? this.BaseQuest : original);
      if (this.CachedQuestParam == null)
        this.CachedQuestParam = this.ConvertQuestParam(original == null ? this.BaseQuest : original);
      return this.CachedQuestParam;
    }

    public QuestParam GetQuestParam() => this.Clone((QuestParam) null, true);

    private QuestParam ConvertQuestParam(QuestParam original)
    {
      if (original == null)
        return (QuestParam) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      JSON_QuestParam json = new JSON_QuestParam();
      json.iname = this.tower_id + "_" + this.floor.ToString();
      json.title = this.title;
      json.name = this.name;
      json.expr = this.expr;
      json.cond = this.cond;
      if (this.cond_floor != 0)
      {
        QuestParam quest = instance.FindQuest(this.tower_id + "_" + (object) this.cond_floor);
        if (quest != null)
          json.cond_quests = new string[1]{ quest.iname };
      }
      json.map = new JSON_MapParam[this.map.Count];
      json.lv = (int) this.lv;
      json.pt = (int) this.pt;
      for (int index = 0; index < this.map.Count; ++index)
        json.map[index] = new JSON_MapParam()
        {
          set = this.map[index].mapSetName,
          scn = this.map[index].mapSceneName,
          bgm = this.map[index].bgmName,
          btl = this.map[index].battleSceneName,
          ev = this.map[index].eventSceneName
        };
      json.area = original.ChapterID;
      json.type = (int) original.type;
      json.notcon = (int) this.notcon;
      json.notitm = 1;
      json.pnum = (int) original.playerNum;
      json.gold = 0;
      json.is_unit_chg = 0;
      json.multi = 1;
      json.me_id = this.me_id;
      json.is_wth_no_chg = this.is_wth_no_chg;
      json.wth_set_id = this.wth_set_id;
      TowerRewardParam towerReward = MonoSingleton<GameManager>.Instance.FindTowerReward(this.reward_id);
      if (towerReward != null)
      {
        List<TowerRewardItem> towerRewardItem1 = towerReward.GetTowerRewardItem();
        for (int index = 0; index < towerRewardItem1.Count; ++index)
        {
          TowerRewardItem towerRewardItem2 = towerRewardItem1[index];
          if (towerRewardItem2 != null && towerRewardItem2.type == TowerRewardItem.RewardType.Gold)
            json.gold = towerRewardItem2.num;
        }
      }
      QuestParam questParam = new QuestParam();
      questParam.Deserialize(json);
      questParam.EntryCondition = original.EntryCondition;
      if (!string.IsNullOrEmpty(this.entry_condition))
      {
        QuestCondParam questCond = MonoSingleton<GameManager>.Instance.FindQuestCond(this.entry_condition);
        if (questCond != null)
          questParam.EntryCondition = questCond;
      }
      questParam.unitNum = (OShort) this.unitnum;
      return questParam;
    }

    public void Deserialize(JSON_MultiTowerFloorParam json)
    {
      this.id = json != null ? json.id : throw new InvalidJSONException();
      this.title = json.title;
      this.name = json.name;
      this.expr = json.expr;
      this.cond = json.cond;
      this.tower_id = json.tower_id;
      this.cond_floor = json.cond_floor;
      this.pt = json.pt;
      this.lv = json.lv;
      this.joblv = json.joblv;
      this.reward_id = json.reward_id;
      this.floor = json.floor;
      this.unitnum = json.unitnum;
      this.notcon = json.notcon;
      this.me_id = json.me_id;
      this.is_wth_no_chg = json.is_wth_no_chg;
      this.wth_set_id = json.wth_set_id;
      this.is_skip = json.is_skip != 0;
      this.iname_text = json.iname;
      this.entry_condition = json.rdy_cnd;
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
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.BaseQuest = instance.FindQuest(this.tower_id);
      QuestParam questParam = this.GetQuestParam();
      instance.AddMTQuest(questParam.iname, questParam);
    }
  }
}
