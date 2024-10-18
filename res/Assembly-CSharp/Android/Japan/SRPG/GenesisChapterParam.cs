// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

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

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public int Priority
    {
      get
      {
        return this.mPriority;
      }
    }

    public string AreaId
    {
      get
      {
        return this.mAreaId;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string BoxIname
    {
      get
      {
        return this.mBoxIname;
      }
    }

    public int ChapterUiIndex
    {
      get
      {
        return this.mChapterUiIndex;
      }
    }

    public string ChapterBanner
    {
      get
      {
        return this.mChapterBanner;
      }
    }

    public string ChapterDetailUrl
    {
      get
      {
        return this.mChapterDetailUrl;
      }
    }

    public string BossHintUrl
    {
      get
      {
        return this.mBossHintUrl;
      }
    }

    public List<GenesisChapterModeInfoParam> ModeInfoList
    {
      get
      {
        if (this.mModeInfo != null)
          return new List<GenesisChapterModeInfoParam>((IEnumerable<GenesisChapterModeInfoParam>) this.mModeInfo);
        return new List<GenesisChapterModeInfoParam>();
      }
    }

    private List<QuestParam> GetQuestTypeList(QuestTypes quest_type, string chapter_id, QuestDifficulties difficulty = QuestDifficulties.MAX, bool is_no_check_unlock = false)
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return questParamList;
      for (int index = 0; index < instance.Quests.Length; ++index)
      {
        QuestParam quest = instance.Quests[index];
        if (quest.type == quest_type && quest.ChapterID == chapter_id && (difficulty == QuestDifficulties.MAX || quest.difficulty == difficulty) && (is_no_check_unlock || quest.IsDateUnlock(-1L)))
          questParamList.Add(quest);
      }
      return questParamList;
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
      if (questTypeList.Count != 0)
        return questTypeList[0];
      return (QuestParam) null;
    }

    public GenesisChapterModeInfoParam GetModeInfo(QuestDifficulties difficulty)
    {
      if (this.mModeInfo == null)
        return (GenesisChapterModeInfoParam) null;
      int index = (int) difficulty;
      if (index < 0 || index >= this.mModeInfo.Length)
        return (GenesisChapterModeInfoParam) null;
      return this.mModeInfo[index];
    }

    public int GetMaxStarNum(List<QuestParam> quest_list)
    {
      int num = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            num += this.GetMaxStarNum(quest);
        }
      }
      return num;
    }

    public int GetNowStarNum(List<QuestParam> quest_list)
    {
      int num = 0;
      if (quest_list != null)
      {
        for (int index = 0; index < quest_list.Count; ++index)
        {
          QuestParam quest = quest_list[index];
          if (quest != null && quest.bonusObjective != null)
            num += this.GetNowStarNum(quest);
        }
      }
      return num;
    }

    public int GetMaxStarNum(QuestParam quest_param)
    {
      if (quest_param == null || quest_param.bonusObjective == null)
        return 0;
      return quest_param.bonusObjective.Length;
    }

    public int GetNowStarNum(QuestParam quest_param)
    {
      if (quest_param == null || quest_param.bonusObjective == null)
        return 0;
      int num = 0;
      for (int index = 0; index < quest_param.bonusObjective.Length; ++index)
      {
        if (quest_param.IsMissionClear(index))
          ++num;
      }
      return num;
    }

    public bool IsBossLiberation(QuestDifficulties difficulty)
    {
      if (this.GetBossQuest(difficulty, false) == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = this.GetModeInfo(difficulty);
      if (modeInfo == null)
        return false;
      List<QuestParam> questList = this.GetQuestList(difficulty, false);
      if (questList.Count == 0)
        return false;
      int index = modeInfo.LiberationQuestNo - 1;
      if (index < 0 || index >= questList.Count)
        index = questList.Count - 1;
      return questList[index].state == QuestStates.Cleared;
    }

    public static void Deserialize(ref List<GenesisChapterParam> list, JSON_GenesisChapterParam[] json)
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
