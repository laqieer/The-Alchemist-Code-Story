// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GenesisChapterParam
  {
    private string mIname;
    private int mPriority;
    private string mAreaId;
    private string mName;
    private string mBoxIname;
    private int mChapterUiIndex;
    private string mChapterBanner;
    private string mChapterDetailUrl;
    private string mBossHintUrl;
    private GenesisChapterModeInfoParam[] mModeInfo;

    public string Iname => this.mIname;

    public int Priority => this.mPriority;

    public string AreaId => this.mAreaId;

    public string Name => this.mName;

    public string BoxIname => this.mBoxIname;

    public int ChapterUiIndex => this.mChapterUiIndex;

    public string ChapterBanner => this.mChapterBanner;

    public string ChapterDetailUrl => this.mChapterDetailUrl;

    public string BossHintUrl => this.mBossHintUrl;

    public List<GenesisChapterModeInfoParam> ModeInfoList
    {
      get
      {
        return this.mModeInfo != null ? new List<GenesisChapterModeInfoParam>((IEnumerable<GenesisChapterModeInfoParam>) this.mModeInfo) : new List<GenesisChapterModeInfoParam>();
      }
    }

    private List<QuestParam> GetQuestTypeList(
      QuestTypes quest_type,
      string chapter_id,
      QuestDifficulties difficulty = QuestDifficulties.MAX,
      bool is_no_check_unlock = false)
    {
      List<QuestParam> questTypeList = new List<QuestParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return questTypeList;
      ChapterParam area = instance.FindArea(chapter_id);
      if (area != null && area.quests != null)
      {
        for (int index = 0; index < area.quests.Count; ++index)
        {
          QuestParam quest = area.quests[index];
          if (quest.type == quest_type && (difficulty == QuestDifficulties.MAX || quest.difficulty == difficulty) && (is_no_check_unlock || quest.IsDateUnlock()))
            questTypeList.Add(quest);
        }
      }
      return questTypeList;
    }

    public void Deserialize(JSON_GenesisChapterParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mPriority = json.priority;
      this.mAreaId = json.area_id;
      this.mName = json.name;
      this.mBoxIname = json.box_iname;
      this.mChapterUiIndex = json.chapter_ui_index;
      this.mChapterBanner = json.chapter_banner;
      this.mChapterDetailUrl = json.chapter_detail_url;
      this.mBossHintUrl = json.boss_hint_url;
      this.mModeInfo = (GenesisChapterModeInfoParam[]) null;
      if (json.mode_info == null || json.mode_info.Length == 0)
        return;
      this.mModeInfo = new GenesisChapterModeInfoParam[json.mode_info.Length];
      for (int index = 0; index < json.mode_info.Length; ++index)
      {
        this.mModeInfo[index] = new GenesisChapterModeInfoParam();
        this.mModeInfo[index].Deserialize(json.mode_info[index]);
      }
    }

    public List<QuestParam> GetQuestList(QuestDifficulties difficulty = QuestDifficulties.MAX, bool is_no_check_unlock = false)
    {
      return this.GetQuestTypeList(QuestTypes.GenesisStory, this.mAreaId, difficulty, is_no_check_unlock);
    }

    public QuestParam GetBossQuest(QuestDifficulties difficulty, bool is_no_check_unlock = false)
    {
      List<QuestParam> questTypeList = this.GetQuestTypeList(QuestTypes.GenesisBoss, this.mAreaId, difficulty, is_no_check_unlock);
      return questTypeList.Count != 0 ? questTypeList[0] : (QuestParam) null;
    }

    public GenesisChapterModeInfoParam GetModeInfo(QuestDifficulties difficulty)
    {
      if (this.mModeInfo == null)
        return (GenesisChapterModeInfoParam) null;
      int index = (int) difficulty;
      return index < 0 || index >= this.mModeInfo.Length ? (GenesisChapterModeInfoParam) null : this.mModeInfo[index];
    }

    public int GetMaxStarNum(List<QuestParam> quest_list)
    {
      int maxStarNum = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            maxStarNum += this.GetMaxStarNum(quest);
        }
      }
      return maxStarNum;
    }

    public int GetNowStarNum(List<QuestParam> quest_list)
    {
      int nowStarNum = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            nowStarNum += this.GetNowStarNum(quest);
        }
      }
      return nowStarNum;
    }

    public int GetMaxStarNum(QuestParam quest_param)
    {
      return quest_param == null || quest_param.bonusObjective == null ? 0 : quest_param.bonusObjective.Length;
    }

    public int GetNowStarNum(QuestParam quest_param)
    {
      if (quest_param == null || quest_param.bonusObjective == null)
        return 0;
      int nowStarNum = 0;
      for (int index = 0; index < quest_param.bonusObjective.Length; ++index)
      {
        if (quest_param.IsMissionClear(index))
          ++nowStarNum;
      }
      return nowStarNum;
    }

    public bool IsBossLiberation(QuestDifficulties difficulty)
    {
      QuestParam bossQuest = this.GetBossQuest(difficulty);
      if (bossQuest == null)
        return false;
      if (bossQuest.cond_quests != null && bossQuest.cond_quests.Length > 0)
        return bossQuest.IsQuestCondition();
      GenesisChapterModeInfoParam modeInfo = this.GetModeInfo(difficulty);
      if (modeInfo == null)
        return false;
      List<QuestParam> questList = this.GetQuestList(difficulty);
      if (questList.Count == 0)
        return false;
      int index = modeInfo.LiberationQuestNo - 1;
      if (index < 0 || index >= questList.Count)
        index = questList.Count - 1;
      return questList[index].state == QuestStates.Cleared;
    }

    public bool IsBossCondQuests(QuestDifficulties difficulty)
    {
      QuestParam bossQuest = this.GetBossQuest(difficulty);
      return bossQuest != null && bossQuest.cond_quests != null && bossQuest.cond_quests.Length > 0;
    }

    public static void Deserialize(
      ref List<GenesisChapterParam> list,
      JSON_GenesisChapterParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<GenesisChapterParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GenesisChapterParam genesisChapterParam = new GenesisChapterParam();
        genesisChapterParam.Deserialize(json[index]);
        list.Add(genesisChapterParam);
      }
      SortUtility.StableSort<GenesisChapterParam>(list, (Comparison<GenesisChapterParam>) ((u1, u2) => u1.mPriority - u2.mPriority));
    }
  }
}
